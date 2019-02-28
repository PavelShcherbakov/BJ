using BJ.DAL.Interfaces;
using BJ.DAL.Repositories;
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
            var gamesResponse = new List<GameGetAllGamesHistoryResponseViewItem>();
            var games = _gameRepository.Find(x => x.UserId == userId).ToList();
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

                    gamesResponse.Add(game);
                });
            
            var response = new GetAllGamesHistoryResponseView();
            response.Games = gamesResponse;
            return response;
        }

        public async Task<GetGameInfoHistoryResponseView> GetGameInfo(string userId, GetGameInfoHistoryView model)
        {

            var response = new GetGameInfoHistoryResponseView();



            return response;
        }


    }
}
