using BJ.BLL.Commons;
using BJ.BLL.Helpers.Interfaces;
using BJ.BLL.Services.Interfaces;
using BJ.DAL.Interfaces;
using BJ.Entities;
using BJ.Entities.Enums;
using BJ.ViewModels.GameViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotRepository _botRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IBotsStepRepository _botsStepRepository;
        private readonly IUsersStepRepository _usersStepRepository;
        private readonly IUsersPointsRepository _usersPointsRepository;
        private readonly IBotsPointsRepository _botsPointsRepository;
        private readonly IDeckHelper _deckHelper;


        public GameService(
            IUserRepository userRepository,
            IBotRepository botRepository,
            IGameRepository gameRepository,
            IDeckRepository deckRepository,
            IUsersStepRepository usersStepRepository,
            IBotsStepRepository botsStepRepository,
            IUsersPointsRepository usersPointsRepository,
            IBotsPointsRepository botsPointsRepository,
            IDeckHelper deckHelper)
        {
            _userRepository = userRepository;
            _botRepository = botRepository;
            _gameRepository = gameRepository;
            _usersStepRepository = usersStepRepository;
            _deckRepository = deckRepository;
            _botsStepRepository = botsStepRepository;
            _usersPointsRepository = usersPointsRepository;
            _botsPointsRepository = botsPointsRepository;
            _deckHelper = deckHelper;
        }

        public async Task<StartGameResponseView> StartGame(string userId, StartGameView model)
        {
            var game = new Game()
            {
                UserId = userId,
                State = UserGameStateType.InGame,
                NumberOfPlayers = model.NumberOfBots + 1
            };
            await _gameRepository.CreateAsync(game);

            var usersPoints = new UsersPoints()
            {
                UserId = userId,
                GameId = game.Id,
                Points = Constants.GameSettings.InitionalPoints
            };
            await _usersPointsRepository.CreateAsync(usersPoints);

            var bots = (await _botRepository.GetRandomBotsAsync(model.NumberOfBots)).ToList();

            var botsPoints = bots.Select(x => new BotsPoints()
            {
                BotId = x.Id,
                GameId = game.Id,
                Points = Constants.GameSettings.InitionalPoints,
                CardsInHand = Constants.GameSettings.InitialCardsInHand
            })
            .ToList();
            await _botsPointsRepository.AddRangeAsync(botsPoints);

            await InitialCardsDeal(game, usersPoints, bots, botsPoints);

            await _gameRepository.UpdateAsync(game);

            var response = new StartGameResponseView()
            {
                GameId = game.Id,
                State = (int)game.State
            };
            return response;
        }

        public async Task<GetStateGameResponseView> GetState(string userId)
        {
            var game = await _gameRepository.GetActiveGameAsync(userId);
            var botsPoints = (await _botsPointsRepository.GetPointsByGameIdAsync(game.Id)).ToList();
            var user = await _userRepository.GetByIdAsync(userId);
            var userSteps = (await _usersStepRepository.GetStepsByGameIdAsync(game.Id)).ToList();

            var response = new GetStateGameResponseView();

            response.GameId = game.Id;

            var bots = new List<BotGetStateGameResponseViewItem>();
            botsPoints.ForEach(
                bp =>
                {
                    bots.Add(new BotGetStateGameResponseViewItem()
                    {
                        CardsInHand = bp.CardsInHand,
                        Name = bp.Bot.Name
                    });
                }
            );
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
                State = new StateGetStateGameResponseView()
                {
                    State = (int)game.State,
                    StateAsString = game.State.ToString()
                }
            };
            response.User = userGetStatusGameViewItem;

            return response;
        }

        public async Task<GetCardGameResponseView> GetCard(string userId)
        {

            GetCardGameResponseView response;

            var game = await _gameRepository.GetActiveGameAsync(userId);
            var usersPoints = await _usersPointsRepository.GetPointsByGameIdAsync(game.Id);
            var botsPoints = (await _botsPointsRepository.GetPointsByGameIdAsync(game.Id)).ToList();

            await RoundCardsDeal(game, usersPoints, botsPoints);

            await _gameRepository.UpdateAsync(game);
            response = await CreateGetCardGameResponseView(userId, game, botsPoints, usersPoints);

            return response;
        }

        private async Task<GetCardGameResponseView> CreateGetCardGameResponseView(string userId, Game game, List<BotsPoints> botsPoints, UsersPoints usersPoints)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var userSteps = (await _usersStepRepository.GetStepsByGameIdAsync(game.Id)).ToList();

            var response = new GetCardGameResponseView();

            response.GameId = game.Id;

            var bots = new List<BotGetCardGameResponseViewItem>();
            botsPoints.ForEach(
                bp =>
                {
                    bots.Add(new BotGetCardGameResponseViewItem()
                    {
                        CardsInHand = bp.CardsInHand,
                        Name = bp.Bot.Name
                    });
                }
            );
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
                State = new StateGetCardGameResponseView()
                {
                    State = (int)game.State,
                    StateAsString = game.State.ToString()
                }
            };
            response.User = userGetStatusGameViewItem;

            return response;
        }

        public async Task<EndGameResponseView> EndGame(string userId)
        {
            var game = await _gameRepository.GetActiveGameAsync(userId);
            var usersPoints = await _usersPointsRepository.GetPointsByGameIdAsync(game.Id);
            var botsPoints = (await _botsPointsRepository.GetPointsByGameIdAsync(game.Id)).ToList();

            await LastCardsDeal(game, usersPoints, botsPoints);

            int WinningPoints = 0;
            botsPoints.ForEach(
                bp =>
                {
                    if (bp.Points > WinningPoints && bp.Points <= Constants.GameSettings.WinningNumber)
                    {
                        WinningPoints = bp.Points;
                    }
                }
           );

            var resultUsersPoints = await _usersPointsRepository.GetPointsByGameIdAsync(game.Id);

            if (resultUsersPoints.Points >= WinningPoints && resultUsersPoints.Points <= Constants.GameSettings.WinningNumber)
            {
                game.State = UserGameStateType.Win;
            }
            else
            {
                game.State = UserGameStateType.Lose;
            }

            await _gameRepository.UpdateAsync(game);

            EndGameResponseView response = await CreateEndGameResponseView(userId, game, usersPoints, botsPoints);

            return response;
        }

        private async Task<EndGameResponseView> CreateEndGameResponseView(string userId, Game game, UsersPoints usersPoints, List<BotsPoints> botsPoints)
        {

            var user = await _userRepository.GetByIdAsync(userId);
            var userSteps = (await _usersStepRepository.GetStepsByGameIdAsync(game.Id)).ToList();

            var response = new EndGameResponseView();

            response.GameId = game.Id;

            var bots = new List<BotEndGameResponseViewItem>();

            botsPoints.ForEach(
                bp =>
                {
                    bots.Add(new BotEndGameResponseViewItem()
                    {
                        CardsInHand = bp.CardsInHand,
                        Name = bp.Bot.Name
                    });
                }
            );

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
                State = new StateEndGameResponseView()
                {
                    State = (int)game.State,
                    StateAsString = game.State.ToString()
                }
            };
            response.User = userGetStatusGameViewItem;

            return response;
        }

        public async Task<HasActiveGameGameResponseView> HasActiveGame(string userId)
        {
            var game = await _gameRepository.GetActiveGameAsync(userId);
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

        private async Task InitialCardsDeal(Game game, UsersPoints usersPoints, List<Bot> bots, List<BotsPoints> botsPoints)
        {

            for (int i = 0; i < Constants.GameSettings.InitialNumOfCard; i++)
            {
                await CardsDeal(game, usersPoints, bots, botsPoints);
            }
            if (game.State == UserGameStateType.Lose)
            {
                await LastCardsDeal(game, usersPoints, botsPoints);
            }

        }

        private async Task RoundCardsDeal(Game game, UsersPoints usersPoints, List<BotsPoints> botsPoints)
        {
            var playingBots = GetPlayingBots(botsPoints);

            await CardsDeal(game, usersPoints, playingBots, botsPoints);

            if (game.State == UserGameStateType.Lose)
            {
                await LastCardsDeal(game, usersPoints, botsPoints);
            }
        }

        private async Task LastCardsDeal(Game game, UsersPoints usersPoints, List<BotsPoints> botsPointsList)
        {
            List<Bot> playingBots;

            playingBots = GetPlayingBots(botsPointsList);
            while (playingBots.Count != 0)
            {
                await CardsDeal(game, usersPoints, playingBots, botsPointsList, false);
                playingBots = GetPlayingBots(botsPointsList);
            }

            var remainingCards = await _deckRepository.GetCardsByGameIdAsync(game.Id);
            await _deckRepository.RemoveRangeAsync(remainingCards);

        }

        private async Task CardsDeal(Game game, UsersPoints usersPoints, List<Bot> bots, List<BotsPoints> botsPointsList, bool userPlaying = true)
        {
            game.CountStep += 1;

            var numOfCards = bots.Count;
            if (userPlaying) numOfCards += Constants.GameSettings.NumOfUserInGame;

            int countCardInDeck = await _deckRepository.GetCountCardsAsync(usersPoints.GameId);
            while (countCardInDeck < numOfCards)
            {
                await AddDeckAsync(usersPoints.GameId);
                countCardInDeck = await _deckRepository.GetCountCardsAsync(usersPoints.GameId);
            }

            List<Card> takenСards = (await _deckRepository.GetRandomCardsByGameIdAsync(usersPoints.GameId, numOfCards)).ToList();


            var usersSteps = new List<UsersStep>();
            if (userPlaying)
            {
                var us = await UserGetCardAsync(game, takenСards, usersPoints, game.CountStep, --numOfCards);
                usersSteps.Add(us);
            }

            var botsSteps = new List<BotsStep>();
            var modifiedBotsPointsList = new List<BotsPoints>();
            bots.ForEach(
                bot =>
                {
                    var botsPoints = botsPointsList.Where(x => x.BotId == bot.Id).FirstOrDefault();

                    var botInfo = BotGetCard(takenСards, botsPoints, game.CountStep, --numOfCards);

                    botsSteps.Add(botInfo.botsStep);
                    modifiedBotsPointsList.Add(botInfo.botsPoints);
                }
            );

            await _botsPointsRepository.UpdateRangeAsync(modifiedBotsPointsList);
            await _usersStepRepository.AddRangeAsync(usersSteps);
            await _botsStepRepository.AddRangeAsync(botsSteps);
            await _deckRepository.RemoveRangeAsync(takenСards);

        }

        private async Task AddDeckAsync(Guid gameId)
        {
            var newDeck = _deckHelper.CreateDeck(gameId);
            await _deckRepository.AddRangeAsync(newDeck);
        }

        private async Task<UsersStep> UserGetCardAsync(Game game, List<Card> cards, UsersPoints usersPoints, int stepNumder, int numCard)
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

            await SetUsersPointsAsync(game, usersPoints, card.Rank);

            return usersStep;
        }

        private async Task SetUsersPointsAsync(Game game, UsersPoints usersPoints, RankType rank)
        {
            usersPoints.Points += (int)rank;
            if (usersPoints.Points > Constants.GameSettings.WinningNumber)
            {
                game.State = UserGameStateType.Lose;
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

        private BotsPoints SetBotsPointsAsync(BotsPoints botsPoints, RankType rank)
        {
            botsPoints.Points += (int)rank;
            botsPoints.CardsInHand++;
            return botsPoints;
        }

        private List<Bot> GetPlayingBots(List<BotsPoints> botsPointsList)
        {
            var playingBots = new List<Bot>();
            botsPointsList.ForEach(
                botsPoints =>
                {
                    if (botsPoints.Points < Constants.GameSettings.BotStopGame)
                    {
                        playingBots.Add(botsPoints.Bot);
                    }
                }
            );

            return playingBots;
        }
    }
}

