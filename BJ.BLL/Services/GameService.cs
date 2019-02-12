using BJ.BLL.Helpers;
using BJ.DAL.Interfaces;
using BJ.Entities;
using BJ.ViewModels.GameViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class GameService
    {
        private IUserRepository _userRepository;
        private readonly IBotRepository _botRepository;
        private readonly IGameRepository _gameRepository;
        private readonly CardService _cardHelper;

        public GameService(IUserRepository userRepository, IBotRepository botRepository, IGameRepository gameRepository, CardService cardHelper)
        {
            _userRepository = userRepository;
            _botRepository = botRepository;
            _gameRepository = gameRepository;
            _cardHelper = cardHelper;
        }

        

        public async Task StartGame(string userId, StartGameView model)
        {
            Game game = new Game() { UserId = userId };
            List<Bot> bots = new List<Bot>();
            for(int i = 0; i < model.NumberOfBots; i++)
            {
                var bot = new Bot()
                {
                    Name = "bot" + i,
                    GameId = game.Id
                };
                bots.Add(bot);
            }
            await _gameRepository.CreateAsync(game);
            await _botRepository.AddRangeAsync(bots);
            await _cardHelper.CreateDecksAsync(model.NumberOfBots, game.Id);
            await _cardHelper.CardsDeal(game.Id, userId, bots);
        }

        public async Task GetCard(string userId, GetCardView model)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<Bot> bots = await _botRepository.FindAsync(x => x.GameId == model.GameId) as List<Bot>;
            await _cardHelper.RoundCardsDeal(model.GameId, userId, bots);
        }

        public async Task EndGame(string userId, EndGameView model)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            List<Bot> bots = await _botRepository.FindAsync(x => x.GameId == model.GameId) as List<Bot>;
            await _cardHelper.RoundCardsDeal(model.GameId, userId, bots);
        }

    }
}
