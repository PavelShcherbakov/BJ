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
        public enum UserGameState { InGame, Lose, Win };

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

        ///////////////////////////////////Public////////////////////////////////////////////////
        
        public async Task<StartGameResponseView> StartGame(string userId, StartGameView model)
        {
            var game = new Game() { UserId = userId };
            await _gameRepository.CreateAsync(game);

            var usersPoints = new UsersPoints()
            {
                UserId = userId,
                GameId = game.Id,
                Points = Constants.InitionalPoints
            };
            await _usersPointsRepository.CreateAsync(usersPoints);
            
            var bots = (await _botRepository.GetRandomBotsAsync(model.NumberOfBots)).ToList();

            var botsPointsList = bots.Select(x => new BotsPoints() 
            {
                BotId = x.Id,
                GameId = game.Id,
                Points = Constants.InitionalPoints
            })
            .ToList();
            await _botsPointsRepository.AddRangeAsync(botsPointsList);

            game.CountStep = await InitialCardsDeal(usersPoints, bots, botsPointsList);
            await _gameRepository.UpdateAsync(game);     
            
            var response = new StartGameResponseView()
            {
                GameId = game.Id
            };
            return response;
        }

        public async Task<GetCardGameResponseView> GetCard(string userId, GetCardGameView model)
        {
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();
            if (usersPoints.Points > Constants.WinningNumber)
            {
                return new GetCardGameResponseView() { State = (int)UserGameState.Lose };
            }

            var game = await _gameRepository.GetByIdAsync(model.GameId);
            var botsPointsList = _botsPointsRepository.Find(x => x.GameId == model.GameId).ToList();           

            game.CountStep = await RoundCardsDeal(usersPoints, botsPointsList, game.CountStep);
            await _gameRepository.UpdateAsync(game);

            var resultUsersPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();

            GetCardGameResponseView response;
            if (resultUsersPoints.Points > Constants.WinningNumber)
            {
                response = new GetCardGameResponseView() { State = (int)UserGameState.Lose };
            }
            else
            {
                response = new GetCardGameResponseView() { State = (int)UserGameState.InGame };
            }
            return response;
        }

        public async Task<EndGameResponseView> EndGame(string userId, EndGameView model)
        {

            var game = await _gameRepository.GetByIdAsync(model.GameId);
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();
            var botsPointsList = _botsPointsRepository.Find(x => x.GameId == model.GameId) as List<BotsPoints>;

            game.CountStep = await LastCardsDeal(usersPoints, botsPointsList, game.CountStep);
            await _gameRepository.UpdateAsync(game);

            var remainingCards = _deckRepository.Find(x => x.GameId == model.GameId);
            await _deckRepository.RemoveRangeAsync(remainingCards);

            int WinningPoints = 0;
            botsPointsList.ForEach(botsPoints =>
            {
                if (botsPoints.Points > WinningPoints && botsPoints.Points <= Constants.WinningNumber)
                {
                    WinningPoints = botsPoints.Points;
                }
            });
            
            var resultUsersPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();
            EndGameResponseView response;
            if (resultUsersPoints.Points > WinningPoints && resultUsersPoints.Points <= Constants.WinningNumber)
            {
                response = new EndGameResponseView() { State = (int)UserGameState.Win };
            }
            else
            {
                response = new EndGameResponseView() { State = (int)UserGameState.Lose };
            }

            return response;
        }

        ///////////////////////////////////Private////////////////////////////////////////////////
        
        ///////////////////////////////////CardsDeal////////////////////////////////////////////////


        private async Task<int> InitialCardsDeal( UsersPoints usersPoints, List<Bot> bots,  List<BotsPoints> botsPointsList)
        {
            int stepCount = Constants.InitialStep;
            for (int i = 0; i < Constants.InitialNumOfCard; i++)
            {
                stepCount = await CardsDeal( usersPoints, bots, botsPointsList, stepCount, true);
            }
            return stepCount;
        }

        private async Task<int> RoundCardsDeal( UsersPoints usersPoints, List<BotsPoints> botsPointsList, int stepNumber)
        {

            var playingBots = GetPlayingBots(botsPointsList);
            var countStep = await CardsDeal(usersPoints, playingBots, botsPointsList, stepNumber, true);
            return countStep;

        }

        private async Task<int> LastCardsDeal( UsersPoints usersPoints, List<BotsPoints> botsPointsList, int stepNumber)
        {
            List<Bot> playingBots;
            var countStep = stepNumber;

            playingBots = GetPlayingBots(botsPointsList);
            while (playingBots.Count != 0)
            {
                countStep = await CardsDeal(usersPoints, playingBots, botsPointsList, stepNumber, false);
                playingBots = GetPlayingBots(botsPointsList);
            }

            return countStep;
        }

        private async Task<int> CardsDeal( UsersPoints usersPoints, List<Bot> bots, List<BotsPoints> botsPointsList, int stepNumber, bool userPlaying)
        {
            var usersSteps = new List<UsersStep>();
            var botsSteps = new List<BotsStep>();
            int stepNum = stepNumber;

            var numOfCards = bots.Count;
            if (userPlaying) numOfCards += Constants.NumOfUserInGame;

            int countCardInDeck = await _deckRepository.GetCountCardsAsync(usersPoints.GameId);
            while (countCardInDeck < numOfCards)
            {
                await AddDeckAsync(usersPoints.GameId);
                countCardInDeck = await _deckRepository.GetCountCardsAsync(usersPoints.GameId);
            }

            List<Card> takenСards = (await _deckRepository.GetRandomCardsByGameId(usersPoints.GameId, numOfCards)).ToList();

            if (userPlaying)
            {
                var us = await UserGetCardAsync(takenСards, usersPoints, ++stepNum, --numOfCards);
                usersSteps.Add(us);
            }

            var newBotsSteps = new List<BotsStep>();
            var modifiedBotsPointsList = new List<BotsPoints>();
            bots.ForEach(bot =>
            {
                var botsPoints = botsPointsList.Where(x => x.BotId == bot.Id).FirstOrDefault();

                var botInfo = BotGetCard(takenСards, botsPoints, ++stepNum, --numOfCards);

                botsSteps.Add(botInfo.botsStep);
                modifiedBotsPointsList.Add(botInfo.botsPoints);
            });

            await _botsPointsRepository.UpdateRangeAsync(modifiedBotsPointsList);
            await _usersStepRepository.AddRangeAsync(usersSteps);
            await _botsStepRepository.AddRangeAsync(botsSteps);
            await _deckRepository.RemoveRangeAsync(takenСards);

            return stepNum;
        }

        ///////////////////////////////////Additional////////////////////////////////////////////////

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
        
        private async Task<UsersStep> UserGetCardAsync(List<Card> cards, UsersPoints usersPoints, int stepNumder,int numCard)
        {
            var card = cards.ElementAt(numCard);

            var usersStep = new UsersStep()
            {
                GameId = usersPoints.GameId,
                UserId = usersPoints.UserId,
                StepNumder = stepNumder,
                Rank = card.Rank,
                Suit = card.Suit
            };

            await SetUsersPointsAsync(usersPoints, card.Rank);

            return usersStep;
        }

        private async Task SetUsersPointsAsync(UsersPoints usersPoints, Rank rank)
        {
            usersPoints.Points += (int)rank;
            await _usersPointsRepository.UpdateAsync(usersPoints);
        }

        private (BotsStep botsStep, BotsPoints botsPoints) BotGetCard(List<Card> cards, BotsPoints botsPoints, int stepNumder, int numCard)
        {
            var card = cards.ElementAt(numCard);

            var botsStep = new BotsStep()
            {
                GameId = botsPoints.GameId,
                BotId = botsPoints.BotId,
                StepNumder = stepNumder,
                Rank = card.Rank,
                Suit = card.Suit,
            };
            botsPoints.Points += (int)card.Rank;
            return (botsStep, botsPoints);
        }

        private async Task SetBotsPointsAsync(BotsPoints botsPoints, Rank rank)
        {
            botsPoints.Points += (int)rank;
            await _botsPointsRepository.UpdateAsync(botsPoints);
        }

        private List<Bot> GetPlayingBots(List<BotsPoints> botsPointsList)
        {
            var playingBots = new List<Bot>();
            botsPointsList.ForEach(botsPoints => 
            {
                if (botsPoints.Points < Constants.BotStopGame)
                {
                    playingBots.Add(botsPoints.Bot);
                }
            });

            return playingBots;
        }
    }
}
