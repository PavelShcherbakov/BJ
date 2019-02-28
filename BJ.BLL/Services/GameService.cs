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

        ///////////////////////////////////Public////////////////////////////////////////////////

        public async Task<StartGameResponseView> StartGame(string userId, StartGameView model)
        {
            var game = new Game()
            {
                UserId = userId,
                State = UserGameState.InGame,
                NumberOfPlayers = model.NumberOfBots+1
            };
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
                Points = Constants.InitionalPoints,
                CardsInHand = Constants.InitialCardsInHand
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

        public async Task<GetStateGameResponseView> GetState(string userId)
        {
            var game = _gameRepository.Find(x => x.UserId == userId && (int)x.State == (int)UserGameState.InGame).FirstOrDefault();
            var botsPoints = _botsPointsRepository.Find(x => x.GameId == game.Id).ToList();
            var user = await _userRepository.GetByIdAsync(userId);
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == game.Id).FirstOrDefault();
            var userSteps = _usersStepRepository.Find(x => x.UserId == userId && x.GameId == game.Id).ToList();

            var response = new GetStateGameResponseView();
            var bots = new List<BotGetStateGameResponseViewItem>();
            foreach (var bp in botsPoints)
            {
                bots.Add(new BotGetStateGameResponseViewItem()
                {
                    CardsInHand = bp.CardsInHand,
                    Name = bp.Bot.Name
                });
            };
            response.Bots = bots;
            var userName = user.UserName;
            var cards = new List<CardGetStateGameResponseViewItem>();
            userSteps.ForEach(
                x => cards.Add(new CardGetStateGameResponseViewItem()
                {
                    Rank = (int)x.Rank,
                    Suit = (int)x.Suit
                })
            );
            var userGetStatusGameViewItem = new UserGetStateGameResponseView()
            {
                Name = userName,
                Cards = cards,
                State = (int)game.State
            };
            response.User = userGetStatusGameViewItem;

            return response;
        }



        public async Task<GetCardGameResponseView> GetCard(string userId)
        {

            GetCardGameResponseView response;

            var game = _gameRepository.Find(x => x.UserId == userId && (int)x.State == (int)UserGameState.InGame).FirstOrDefault(); 
            if (game.State==UserGameState.Lose)
            {
                response = await CreateGetCardGameResponseView(userId, game);
                return response;
            }

            var usersPoints = _usersPointsRepository.Find(x => x.GameId == game.Id).FirstOrDefault();
            var botsPointsList = _botsPointsRepository.Find(x => x.GameId == game.Id).ToList();

            await RoundCardsDeal(usersPoints, botsPointsList, game.CountStep);

            response = await CreateGetCardGameResponseView(userId, game);

            return response;
        }


        private async Task<GetCardGameResponseView> CreateGetCardGameResponseView(string userId, Game game)
        {
            var botsPoints = _botsPointsRepository.Find(x => x.GameId == game.Id).ToList();
            var user = await _userRepository.GetByIdAsync(userId);
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == game.Id).FirstOrDefault();
            var userSteps = _usersStepRepository.Find(x => x.UserId == userId && x.GameId == game.Id).ToList();

            var response = new GetCardGameResponseView();
            var bots = new List<BotGetCardGameResponseViewItem>();
            foreach (var bp in botsPoints)
            {
                bots.Add(new BotGetCardGameResponseViewItem()
                {
                    CardsInHand = bp.CardsInHand,
                    Name = bp.Bot.Name
                });
            };
            response.Bots = bots;
            var userName = user.UserName;
            var cards = new List<CardGetCardGameResponseViewItem>();
            userSteps.ForEach(
                x => cards.Add(new CardGetCardGameResponseViewItem()
                {
                    Rank = (int)x.Rank,
                    Suit = (int)x.Suit
                })
            );
            var userGetStatusGameViewItem = new UserGetCardGameResponseView()
            {
                Name = userName,
                Cards = cards,
                State = (int)game.State
            };
            response.User = userGetStatusGameViewItem;

            return response;
        }


        public async Task<EndGameResponseView> EndGame(string userId)
        {
            var game = _gameRepository.Find(x => x.UserId == userId && (int)x.State == (int)UserGameState.InGame).FirstOrDefault();
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == game.Id).FirstOrDefault();
            var botsPointsList = _botsPointsRepository.Find(x => x.GameId == game.Id) as List<BotsPoints>;

            game.CountStep = await LastCardsDeal(usersPoints, botsPointsList, game.CountStep);



            var remainingCards = _deckRepository.Find(x => x.GameId == game.Id);
            await _deckRepository.RemoveRangeAsync(remainingCards);

            int WinningPoints = 0;
            botsPointsList.ForEach(botsPoints =>
            {
                if (botsPoints.Points > WinningPoints && botsPoints.Points <= Constants.WinningNumber)
                {
                    WinningPoints = botsPoints.Points;
                }
            });

            var resultUsersPoints = _usersPointsRepository.Find(x => x.GameId == game.Id).FirstOrDefault();
            
            if (resultUsersPoints.Points > WinningPoints && resultUsersPoints.Points <= Constants.WinningNumber)
            {
                game.State = UserGameState.Win;
            }
            else
            {
                game.State = UserGameState.Lose;
            }

            await _gameRepository.UpdateAsync(game);

            EndGameResponseView response = await CreateEndGameResponseView(userId, game);

            return response;
        }

        private async Task<EndGameResponseView> CreateEndGameResponseView(string userId, Game game)
        {
            
            var botsPoints = _botsPointsRepository.Find(x => x.GameId == game.Id).ToList();
            var user = await _userRepository.GetByIdAsync(userId);
            var usersPoints = _usersPointsRepository.Find(x => x.GameId == game.Id).FirstOrDefault();
            var userSteps = _usersStepRepository.Find(x => x.UserId == userId && x.GameId == game.Id).ToList();

            var response = new EndGameResponseView();
            var bots = new List<BotEndGameResponseViewItem>();
            foreach (var bp in botsPoints)
            {
                bots.Add(new BotEndGameResponseViewItem()
                {
                    CardsInHand = bp.CardsInHand,
                    Name = bp.Bot.Name
                });
            };
            response.Bots = bots;
            var userName = user.UserName;
            var cards = new List<CardEndGameResponseViewItem>();
            userSteps.ForEach(
                x => cards.Add(new CardEndGameResponseViewItem()
                {
                    Rank = (int)x.Rank,
                    Suit = (int)x.Suit
                })
            );
            var userGetStatusGameViewItem = new UserEndGameResponseView()
            {
                Name = userName,
                Cards = cards,
                State = (int)game.State
            };
            response.User = userGetStatusGameViewItem;

            return response;
        }


        ///////////////////////////////////Private////////////////////////////////////////////////

        ///////////////////////////////////CardsDeal////////////////////////////////////////////////


        private async Task<int> InitialCardsDeal(UsersPoints usersPoints, List<Bot> bots, List<BotsPoints> botsPointsList)
        {
            int stepCount = Constants.InitialStep;
            for (int i = 0; i < Constants.InitialNumOfCard; i++)
            {
                stepCount = await CardsDeal(usersPoints, bots, botsPointsList, stepCount, true);
            }
            return stepCount;
        }

        private async Task<int> RoundCardsDeal(UsersPoints usersPoints, List<BotsPoints> botsPointsList, int stepNumber)
        {

            var playingBots = GetPlayingBots(botsPointsList);
            var countStep = await CardsDeal(usersPoints, playingBots, botsPointsList, stepNumber, true);
            return countStep;

        }

        private async Task<int> LastCardsDeal(UsersPoints usersPoints, List<BotsPoints> botsPointsList, int stepNumber)
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

        private async Task<int> CardsDeal(UsersPoints usersPoints, List<Bot> bots, List<BotsPoints> botsPointsList, int stepNumber, bool userPlaying)
        {
            var usersSteps = new List<UsersStep>();
            var botsSteps = new List<BotsStep>();
            int stepNum = ++stepNumber;
            var game = await _gameRepository.GetByIdAsync(usersPoints.GameId);

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
                var us = await UserGetCardAsync(takenСards, usersPoints, stepNum, --numOfCards);
                usersSteps.Add(us);
            }

            var newBotsSteps = new List<BotsStep>();
            var modifiedBotsPointsList = new List<BotsPoints>();
            bots.ForEach(bot =>
            {
                var botsPoints = botsPointsList.Where(x => x.BotId == bot.Id).FirstOrDefault();

                var botInfo = BotGetCard(takenСards, botsPoints, stepNum, --numOfCards);

                botsSteps.Add(botInfo.botsStep);
                modifiedBotsPointsList.Add(botInfo.botsPoints);
            });

            game.CountStep = stepNum;
            await _gameRepository.UpdateAsync(game);
            await _botsPointsRepository.UpdateRangeAsync(modifiedBotsPointsList);
            await _usersStepRepository.AddRangeAsync(usersSteps);
            await _botsStepRepository.AddRangeAsync(botsSteps);
            await _deckRepository.RemoveRangeAsync(takenСards);

            return stepNum;
        }

        public async Task<HasActiveGameGameResponseView> HasActiveGame(string userId)
        {
            var game = _gameRepository.Find(x => x.UserId == userId && (int)x.State == (int)UserGameState.InGame).FirstOrDefault();
            var response = new HasActiveGameGameResponseView();
            if (game == null)
            {
                response.HasActiveGame = false;
                return response;
            }
            else
            {
                response.HasActiveGame = true;
                return response;
            }
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

        private async Task<UsersStep> UserGetCardAsync(List<Card> cards, UsersPoints usersPoints, int stepNumder, int numCard)
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
            if (usersPoints.Points > Constants.WinningNumber)
            {
                var game = await _gameRepository.GetByIdAsync(usersPoints.GameId);
                game.State = UserGameState.Lose;
                await _gameRepository.UpdateAsync(game);
            }
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
            botsPoints = SetBotsPointsAsync(botsPoints, card.Rank);
            return (botsStep, botsPoints);
        }

        private BotsPoints SetBotsPointsAsync(BotsPoints botsPoints, Rank rank)
        {
            botsPoints.Points += (int)rank;
            botsPoints.CardsInHand++;
            return botsPoints;
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
