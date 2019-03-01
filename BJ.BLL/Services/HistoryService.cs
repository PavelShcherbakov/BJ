using BJ.BLL.Commons;
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
    public class HistoryService
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
                        Result = (int)x.State
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


            userSteps.ForEach(
                x =>
                {
                    var pi = new PlayerInfoGetGameInfoHistoryResponseViewItem()
                    {
                        Name = userName,
                        Suit = (int)x.Suit,
                        Rank = (int)x.Rank
                    };
                    response.Steps.ElementAt(x.StepNumder - 1).PlayerInfo.Add(pi);
                });

            botSteps.ForEach(
                x =>
                {
                    var pi = new PlayerInfoGetGameInfoHistoryResponseViewItem()
                    {
                        Name = x.Bot.Name,
                        Suit = (int)x.Suit,
                        Rank = (int)x.Rank
                    };
                    response.Steps.ElementAt(x.StepNumder - 1).PlayerInfo.Add(pi);
                });


            response.Summary = new List<PlayersSummaryGetGameInfoHistoryResponseViewItem>();

            var botPoints = _botsPointsRepository.Find(x => x.GameId == model.GameId).ToList();
            var userPoints = _usersPointsRepository.Find(x => x.GameId == model.GameId).FirstOrDefault();

            var winningPoints = Constants.InitionalPoints;

            botPoints.ForEach(
                bp =>
                {
                    if (bp.Points > winningPoints && bp.Points <= Constants.WinningNumber)
                    {
                        winningPoints = bp.Points;
                    }
                });

            if (userPoints.Points > winningPoints && userPoints.Points <= Constants.WinningNumber)
            {
                winningPoints = userPoints.Points;
            }

            response.Summary.Add(new PlayersSummaryGetGameInfoHistoryResponseViewItem()
            {
                Name = userName,
                Points = userPoints.Points,
                State = userPoints.Points == winningPoints ? (int)UserGameState.Win : (int)UserGameState.Lose
            });

            botPoints.ForEach(
                bp =>
                {
                    response.Summary.Add(new PlayersSummaryGetGameInfoHistoryResponseViewItem()
                    {
                        Name = bp.Bot.Name,
                        Points = bp.Points,
                        State = bp.Points == winningPoints ? (int)UserGameState.Win : (int)UserGameState.Lose
                    });
                });

            return response;
        }
    }
}
