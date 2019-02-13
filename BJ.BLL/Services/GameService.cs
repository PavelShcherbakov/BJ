using BJ.BLL.Commons;
using BJ.BLL.Helpers;
using BJ.DAL.Interfaces;
using BJ.Entities;
using BJ.ViewModels.GameViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class GameService
    {
        private static readonly List<Rank> _ranks;
        private static readonly List<Suit> _suits;

        static GameService()
        {
            _ranks = GetRanks();
            _suits = GetSuits();
        }

        private static List<Rank> GetRanks()
        {
            var ranks = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToList();
            return ranks;
        }
        private static List<Suit> GetSuits()
        {
            return Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();
        }


        private readonly IDeckRepository _deckRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotRepository _botRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IBotsStepRepository _botsStepRepository;
        private readonly IUsersStepRepository _usersStepRepository;


        public GameService(
            IUserRepository userRepository,
            IBotRepository botRepository,
            IGameRepository gameRepository,
            IDeckRepository deckRepository,
            IUsersStepRepository usersStepRepository,
            IBotsStepRepository botsStepRepository)
        {
            _userRepository = userRepository;
            _botRepository = botRepository;
            _gameRepository = gameRepository;
            _usersStepRepository = usersStepRepository;
            _deckRepository = deckRepository;
            _botsStepRepository = botsStepRepository;
        }

        public async Task<StartGameResponseView> StartGame(string userId, StartGameView model)
        {
            Game game = new Game() { UserId = userId, CountStep=0};
            List<Bot> bots = new List<Bot>();
            for(int i = 0; i < model.NumberOfBots; i++)
            {
                var bot = new Bot()
                {
                    Name = "bot" + i,
                    GameId = game.Id,
                    Points = Constants.InitionalPoints
                };
                bots.Add(bot);
            }
            await _gameRepository.CreateAsync(game);
            await _botRepository.AddRangeAsync(bots);
            game.CountStep = await InitialCardsDeal(game.Id, userId, bots);
            await _gameRepository.UpdateAsync(game);
            var response = new StartGameResponseView() { GameId = game.Id };
            return response;

        }

        public async Task<GetCardGameResponseView> GetCard(string userId, GetCardGameView model)
        {
            if (await GetUserPoints(userId)>Constants.WinningNumber) return new GetCardGameResponseView() { State = "Lose" };

            var game = await _gameRepository.GetByIdAsync(model.GameId);
            List<Bot> bots = await _botRepository.FindAsync(x => x.GameId == model.GameId) as List<Bot>;
            var countStep = game.CountStep;
            countStep = await RoundCardsDeal(model.GameId, userId, bots, countStep);
            game.CountStep = countStep;
            await _gameRepository.UpdateAsync(game);

            GetCardGameResponseView response;
            if (await GetUserPoints(userId) > Constants.WinningNumber) response = new GetCardGameResponseView() { State = "Lose" };
            else response = new GetCardGameResponseView() { State = "In game" };

            return response;
        }

        public async Task<EndGameResponseView> EndGame(string userId, EndGameView model)
        {
           
            var game = await _gameRepository.GetByIdAsync(model.GameId);
            List<Bot> bots = await _botRepository.FindAsync(x => x.GameId == model.GameId) as List<Bot>;
            var countStep = game.CountStep;
            countStep = await LastCardsDeal(model.GameId, userId, bots, countStep);
            game.CountStep = countStep;
            await _gameRepository.UpdateAsync(game);

            int WinningPoints = 0;
            foreach (var bot in bots)
            {
                if (bot.Points > WinningPoints && bot.Points <= Constants.WinningNumber) WinningPoints = bot.Points;
            }

            EndGameResponseView response;
            int userPoints = await GetUserPoints(userId);
            if (userPoints < WinningPoints && userPoints > Constants.WinningNumber) response = new EndGameResponseView() { state = "Lose" };
            else response = new EndGameResponseView() { state = "Win" };

            return response;
        }

        private async Task<int> GetUserPoints(string userId)
        {
            int sum = 0;
            var usersSteps = await _usersStepRepository.FindAsync(x => x.UserId == userId) as List<UsersStep>;
            foreach (var userStep in usersSteps)
            {
                sum += (int)userStep.Rank;
            }
            return sum;
        }






        ///////////////////////////////////CardsDeal////////////////////////////////////////////////


        private async Task<int> InitialCardsDeal(Guid gameId, string userId, List<Bot> bots)
        {
            int stepCount = Constants.InitialStep;
            for (int i = 0; i < Constants.InitialNumOfCard; i++)
            {
                stepCount = await CardsDeal(gameId, userId, bots, stepCount, true);
            }
            return stepCount;
        }

        private async Task<int> RoundCardsDeal(Guid gameId, string userId, List<Bot> bots, int stepNumber)
        {

            var playingBots = GetPlayingBots(bots);
            var countStep = await CardsDeal(gameId, userId, playingBots, stepNumber, true);
            return countStep;

        }

        private async Task<int> LastCardsDeal(Guid gameId, string userId, List<Bot> bots, int stepNumber)
        {
            List<Bot> playingBots;
            var countStep = stepNumber;

            playingBots = GetPlayingBots(bots);
            while (playingBots.Count != 0)
            {
                countStep = await CardsDeal(gameId, userId, playingBots, countStep, false);
                playingBots = GetPlayingBots(bots);
            }

            return countStep;
        }

        ///////////////////////////////////Private////////////////////////////////////////////////

        private async Task AddDeckAsync(Guid gameId)
        {
            List<Card> deck = new List<Card>();

            for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(Suit)).Length; j++)
                {
                    deck.Add(new Card() { GameId = gameId, Rank = _ranks[i], Suit = _suits[j] });
                }
            }
            await _deckRepository.AddRangeAsync(deck);
        }

        //private int HowManyDecks(int numOfBots)
        //{
        //    var numOfDecks = (numOfBots + Constants.NumOfUserInGame) / (Constants.NumOfCardsInDeck / Constants.InitialNumOfCard) + 1;
        //    return numOfDecks;
        //}

        private async Task<int> CardsDeal(Guid gameId, string userId, List<Bot> bots, int stepNumber, bool userPlaying)
        {
            var usersSteps = new List<UsersStep>();
            var botsSteps = new List<BotsStep>();
            int stepNum = stepNumber;

            var numOfCards = bots.Count;
            if (userPlaying) numOfCards += Constants.NumOfUserInGame;

            int countCardInDeck = await _deckRepository.GetCountCardsAsync(gameId);
            while (countCardInDeck < numOfCards)
            {
                await AddDeckAsync(gameId);
                countCardInDeck = await _deckRepository.GetCountCardsAsync(gameId);
            }

            List<Card> takenСards = await _deckRepository.GetRandomCardsByGameId(gameId, numOfCards) as List<Card>;

            if (userPlaying)
            {
                var us = await UserGetCardAsync(takenСards, gameId, userId, ++stepNum);
                usersSteps.Add(us);
            }

            foreach (var bot in bots)
            {
                bot.Points += (int)takenСards.Last().Rank;
                var bs = await BotGetCardAsync(takenСards, gameId, bot.Id, ++stepNum);
                botsSteps.Add(bs);
                await _botRepository.UpdateAsync(bot);
            }

            await _usersStepRepository.AddRangeAsync(usersSteps);
            await _botsStepRepository.AddRangeAsync(botsSteps);
            return stepNum;
        }

        private async Task<UsersStep> UserGetCardAsync(List<Card> cards, Guid gameId, string userId, int stepNumder)
        {
            var card = cards.Last();

            var usersStep = new UsersStep()
            {
                GameId = gameId,
                UserId = userId,
                StepNumder = stepNumder,
                Rank = card.Rank,
                Suit = card.Suit
            };

            await _deckRepository.RemoveAsync(card);
            cards.Remove(card);

            return usersStep;
        }

        private async Task<BotsStep> BotGetCardAsync(List<Card> cards, Guid gameId, Guid botId, int stepNumder)
        {
            var card = cards.Last();
            var botsStep = new BotsStep()
            {
                GameId = gameId,
                BotId = botId,
                StepNumder = stepNumder,
                Rank = card.Rank,
                Suit = card.Suit,
            };
            await _deckRepository.RemoveAsync(card);
            cards.Remove(card);
            return botsStep;
        }

        //private async Task<bool> DoesBotNeedCardAsync(Guid botId)
        //{
        //    int sum = 0;
        //    var botsSteps = await _botsStepRepository.FindAsync(x => x.BotId == botId) as List<BotsStep>;
        //    foreach(var botStep in botsSteps)
        //    {
        //        sum += (int)botStep.Rank;
        //    }
        //    if (sum < Constants.BotStopGame) return true;
        //    else return false;
        //}

        private List<Bot> GetPlayingBots(List<Bot> bots)
        {
            var playingBots = new List<Bot>();

            foreach (var bot in bots)
            {
                if (bot.Points < Constants.BotStopGame) playingBots.Add(bot);
            }
            return playingBots;
        }
    }
}
