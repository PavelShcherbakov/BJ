using BJ.BLL.Commons;
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
        private readonly IUsersPointsRepository _usersPointsRepository;
        private readonly IBotsPointsRepository _botsPointsRepository;



        public GameService(
            IUserRepository userRepository,
            IBotRepository botRepository,
            IGameRepository gameRepository,
            IDeckRepository deckRepository,
            IUsersStepRepository usersStepRepository,
            IBotsStepRepository botsStepRepository,
            IUsersPointsRepository usersPointsRepository,
            IBotsPointsRepository botsPointsRepository)
        {
            _userRepository = userRepository;
            _botRepository = botRepository;
            _gameRepository = gameRepository;
            _usersStepRepository = usersStepRepository;
            _deckRepository = deckRepository;
            _botsStepRepository = botsStepRepository;
            _usersPointsRepository = usersPointsRepository;
            _botsPointsRepository = botsPointsRepository;
        }

        public async Task<StartGameResponseView> StartGame(string userId, StartGameView model)
        {
            Game game = new Game() { UserId = userId };

            UsersPoints userPoints = new UsersPoints()
            {
                UserId = userId,
                GameId = game.Id,
                Points = Constants.InitionalPoints
            };

            List<Bot> bots = await _botRepository.GetRandomBotsAsync(model.NumberOfBots) as List<Bot>;
            List<BotsPoints> botsPointsList = new List<BotsPoints>();
            foreach (var bot in bots)
            {
                BotsPoints botsPoints = new BotsPoints()
                {
                    BotId = bot.Id,
                    GameId = game.Id,
                    Points = Constants.InitionalPoints
                };
                botsPointsList.Add(botsPoints);
            }

            await _gameRepository.CreateAsync(game);
            await _usersPointsRepository.CreateAsync(userPoints);
            await _botsPointsRepository.AddRangeAsync(botsPointsList);

            game.CountStep = await InitialCardsDeal(game.Id, userId, userPoints, bots, botsPointsList);
            await _gameRepository.UpdateAsync(game);
            var response = new StartGameResponseView() { GameId = game.Id };
            return response;

        }

        public async Task<GetCardGameResponseView> GetCard(string userId, GetCardGameView model)
        {
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();
            if (usersPoints.Points > Constants.WinningNumber) return new GetCardGameResponseView() { State = "Lose" };
            var game = await _gameRepository.GetByIdAsync(model.GameId);
            var botsPointsList = _botsPointsRepository.Find(x => x.GameId == model.GameId) as List<BotsPoints>;
            List<Bot> bots = new List<Bot>();
            foreach (var botsPoints in botsPointsList)
            {
                bots.Add(botsPoints.Bot);
            }
            
            var countStep = game.CountStep;
            countStep = await RoundCardsDeal(model.GameId, userId, usersPoints, botsPointsList, countStep);
            game.CountStep = countStep;
            await _gameRepository.UpdateAsync(game);

            GetCardGameResponseView response;
            if (usersPoints.Points > Constants.WinningNumber) response = new GetCardGameResponseView() { State = "Lose" };
            else response = new GetCardGameResponseView() { State = "In game" };

            return response;
        }

        public async Task<EndGameResponseView> EndGame(string userId, EndGameView model)
        {

            var game = await _gameRepository.GetByIdAsync(model.GameId);
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();
            var botsPointsList = _botsPointsRepository.Find(x => x.GameId == model.GameId) as List<BotsPoints>;
            List<Bot> bots = new List<Bot>();
            foreach (var botsPoints in botsPointsList)
            {
                bots.Add(botsPoints.Bot);
            }
            var countStep = game.CountStep;
            countStep = await LastCardsDeal(model.GameId, userId, usersPoints, botsPointsList, countStep);
            game.CountStep = countStep;
            await _gameRepository.UpdateAsync(game);

            int WinningPoints = 0;
            foreach (var botsPoints in botsPointsList)
            {
                if (botsPoints.Points > WinningPoints && botsPoints.Points <= Constants.WinningNumber) WinningPoints = botsPoints.Points;
            }

            EndGameResponseView response;
            
            if (usersPoints.Points < WinningPoints && usersPoints.Points > Constants.WinningNumber) response = new EndGameResponseView() { state = "Lose" };
            else response = new EndGameResponseView() { state = "Win" };

            return response;
        }

        //private int GetUserPoints(Guid gameId)
        //{
        //    var userPoints = _usersPointsRepository.Find(x => x.GameId == gameId).FirstOrDefault().Points;
        //    return userPoints;
        //}






        ///////////////////////////////////CardsDeal////////////////////////////////////////////////


        private async Task<int> InitialCardsDeal(Guid gameId, string userId, UsersPoints usersPoints, List<Bot> bots,  List<BotsPoints> botsPointsList)
        {
            int stepCount = Constants.InitialStep;
            for (int i = 0; i < Constants.InitialNumOfCard; i++)
            {
                stepCount = await CardsDeal(gameId, userId, usersPoints, bots, botsPointsList, stepCount, true);
            }
            return stepCount;
        }

        private async Task<int> RoundCardsDeal(Guid gameId, string userId, UsersPoints usersPoints, List<BotsPoints> botsPointsList, int stepNumber)
        {

            var playingBots = GetPlayingBots(botsPointsList);
            var countStep = await CardsDeal(gameId, userId, usersPoints, playingBots, botsPointsList, stepNumber, true);
            return countStep;

        }

        private async Task<int> LastCardsDeal(Guid gameId, string userId, UsersPoints usersPoints, List<BotsPoints> botsPointsList, int stepNumber)
        {
            List<Bot> playingBots;
            var countStep = stepNumber;

            playingBots = GetPlayingBots(botsPointsList);
            while (playingBots.Count != 0)
            {
                countStep = await CardsDeal(gameId, userId, usersPoints, playingBots, botsPointsList, stepNumber, false);
                playingBots = GetPlayingBots(botsPointsList);
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

        private async Task<int> CardsDeal(Guid gameId, string userId, UsersPoints usersPoints, List<Bot> bots,  List<BotsPoints> botsPointsList, int stepNumber, bool userPlaying)
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
                var us = await UserGetCardAsync(takenСards, gameId, userId, usersPoints,++stepNum);
                usersSteps.Add(us);
            }

            foreach (var bot in bots)
            {
                var bs = await BotGetCardAsync(takenСards, gameId, bot.Id, ++stepNum);
                botsSteps.Add(bs);
                await _botRepository.UpdateAsync(bot);
            }

            await _usersStepRepository.AddRangeAsync(usersSteps);
            await _botsStepRepository.AddRangeAsync(botsSteps);
            return stepNum;
        }

        private async Task<UsersStep> UserGetCardAsync(List<Card> cards, Guid gameId, string userId,UsersPoints usersPoints, int stepNumder)
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
            await SetUsersPointsAsync(gameId, card.Rank);
            await _deckRepository.RemoveAsync(card);
            cards.Remove(card);
            return usersStep;
        }

        private async Task SetUsersPointsAsync(Guid gameId, Rank rank)
        {
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == gameId).FirstOrDefault();
            usersPoints.Points += (int)rank;
            await _usersPointsRepository.UpdateAsync(usersPoints);
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
            await SetBotsPointsAsync(gameId, botId, card.Rank);
            await _deckRepository.RemoveAsync(card);
            cards.Remove(card);
            return botsStep;
        }

        private async Task SetBotsPointsAsync(Guid gameId, Guid botId, Rank rank)
        {
            var botsPoints = _botsPointsRepository.Find(x => x.GameId == gameId).Where(x => x.BotId == botId).FirstOrDefault();
            botsPoints.Points += (int)rank;
            await _botsPointsRepository.UpdateAsync(botsPoints);
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

        private List<Bot> GetPlayingBots(List<BotsPoints> botsPointsList)
        {
            var playingBots = new List<Bot>();
            foreach (var botsPoints in botsPointsList)
            {
                if(botsPoints.Points<Constants.BotStopGame) playingBots.Add(botsPoints.Bot);
            }
            return playingBots;
        }
    }
}
