using BJ.BLL.Commons;
using BJ.BLL.Services.Interfaces;
using BJ.DAL.Interfaces;
using BJ.DAL.Repositories;
using BJ.Entities;
using BJ.Entities.Enums;
using BJ.ViewModels.HistoryView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class HistoryService: IHistoryService
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotRepository _botRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IBotsStepRepository _botsStepRepository;
        private readonly IUsersStepRepository _usersStepRepository;
        private readonly IUsersPointsRepository _usersPointsRepository;
        private readonly IBotsPointsRepository _botsPointsRepository;

        public HistoryService(
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

        public async Task<GetAllGamesHistoryResponseView> GetAllGames(string userId)
        {
            var response = new GetAllGamesHistoryResponseView();
            response.Games = new List<GameGetAllGamesHistoryResponseViewItem>();
            var games = _gameRepository.Find(x => x.UserId == userId && (int)x.State != (int)UserGameState.InGame).ToList();
            games.ForEach(
                x =>
                {
                    var game = new GameGetAllGamesHistoryResponseViewItem()
                    {
                        GameId = x.Id,
                        CreationDate = x.CreationDate,
                        NumberOfPlayers = x.NumberOfPlayers,
                        Result = new ResultGetAllGamesHistoryResponseViewItem()
                        {
                            State = (int)x.State,
                            StateAsString = x.State.ToString()
                        }
                    };

                    response.Games.Add(game);
                });

            return response;
        }

        public async Task<GetGameInfoHistoryResponseView> GetGameInfo(string userId, GetGameInfoHistoryView model)
        {

            var game = await _gameRepository.GetByIdAsync(model.GameId);

            var response = new GetGameInfoHistoryResponseView();


            response.Steps = new List<StepGetGameInfoHistoryResponseViewItem>(game.CountStep);
            for(int i=0;i< game.CountStep; i++)
            {
                response.Steps.Add(new StepGetGameInfoHistoryResponseViewItem());
            }
            response.Steps.ForEach(
                x =>
                {
                    x.PlayerInfo = new List<PlayerInfoGetGameInfoHistoryResponseViewItem>();
                });

            var userSteps = _usersStepRepository.Find(x => x.GameId == model.GameId).ToList();
            var botSteps = _botsStepRepository.Find(x => x.GameId == model.GameId).ToList();
            var userName = (await _userRepository.GetByIdAsync(userId)).UserName;



            //var groupedUserSteps = userSteps.GroupBy(x => x.StepNumder);
            //var groupedBotSteps = botSteps.GroupBy(x => x.StepNumder);






            userSteps.ForEach(
                x =>
                {
                    var pi = new PlayerInfoGetGameInfoHistoryResponseViewItem()
                    {
                        Name = userName,
                        Card = new CardGetGameInfoHistoryResponseView()
                        {
                            Suit =new SuitGetGameInfoHistoryResponseView()
                            {
                                Suit = (int)x.Suit,
                                SuitAsString = x.Suit.ToString()
                            },
                            Rank = new RankGetGameInfoHistoryResponseView()
                            {
                                Rank = (int)x.Rank,
                                RankAsString = x.Rank.ToString()
                            }
                        }
                    };
                    response.Steps.ElementAt(x.StepNumder - 1).PlayerInfo.Add(pi);
                });

            botSteps.ForEach(
                x =>
                {
                    var pi = new PlayerInfoGetGameInfoHistoryResponseViewItem()
                    {
                        Name = x.Bot.Name,
                        Card = new CardGetGameInfoHistoryResponseView()
                        {
                            Suit = new SuitGetGameInfoHistoryResponseView()
                            {
                                Suit = (int)x.Suit,
                                SuitAsString = x.Suit.ToString()
                            },
                            Rank = new RankGetGameInfoHistoryResponseView()
                            {
                                Rank = (int)x.Rank,
                                RankAsString = x.Rank.ToString()
                            }
                        }
                    };
                    response.Steps.ElementAt(x.StepNumder - 1).PlayerInfo.Add(pi);
                });


            response.Summary = new List<PlayersSummaryGetGameInfoHistoryResponseViewItem>();

            var botPoints = _botsPointsRepository.Find(x => x.GameId == model.GameId).ToList();
            var userPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();

            var winningPoints = Constants.GameSettings.InitionalPoints;

            botPoints.ForEach(
                bp =>
                {
                    if (bp.Points > winningPoints && bp.Points <= Constants.GameSettings.WinningNumber)
                    {
                        winningPoints = bp.Points;
                    }
                });

            if (userPoints.Points > winningPoints && userPoints.Points <= Constants.GameSettings.WinningNumber)
            {
                winningPoints = userPoints.Points;
            }

            response.Summary.Add(new PlayersSummaryGetGameInfoHistoryResponseViewItem()
            {
                Name = userName,
                Points = userPoints.Points,
                State = new StateGetGameInfoHistoryResponseView()
                {
                    State = userPoints.Points == winningPoints ? (int)UserGameState.Win : (int)UserGameState.Lose,
                    StateAsString = userPoints.Points == winningPoints ? UserGameState.Win.ToString() : UserGameState.Lose.ToString()

                }
            });

            botPoints.ForEach(
                bp =>
                {
                    response.Summary.Add(new PlayersSummaryGetGameInfoHistoryResponseViewItem()
                    {
                        Name = bp.Bot.Name,
                        Points = bp.Points,
                        State = new StateGetGameInfoHistoryResponseView()
                        {
                            State = bp.Points == winningPoints ? (int)UserGameState.Win : (int)UserGameState.Lose,
                            StateAsString = bp.Points == winningPoints ? UserGameState.Win.ToString() : UserGameState.Lose.ToString()
                        }
                    });
                });

            return response;
        }
    }
}
