using BJ.BLL.Helpers;
using BJ.DAL.Interfaces;
using BJ.Entities;
using BJ.ViewModels.GameViews;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class GameService
    {
        private IUserRepository _userRepository;
        private readonly IBotRepository _botRepository;
        private readonly IGameRepository _gameRepository;
        private readonly CardHelper _cardHelper;

        public GameService(IUserRepository userRepository, IBotRepository botRepository, IGameRepository gameRepository, CardHelper cardHelper)
        {
            _userRepository = userRepository;
            _botRepository = botRepository;
            _gameRepository = gameRepository;
            _cardHelper = cardHelper;
        }

        

        public async Task StartGame(string userId, StartGameView model)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            Game game = new Game() { User = user};
            Bot[] bots = new Bot[model.NumberOfBots];
            for(int i = 0; i < bots.Length; i++)
            {
                bots[i] = new Bot();
                bots[i].Name = "bot" + i;
                bots[i].Game = game;
            }
            await _gameRepository.CreateAsync(game);
            await _botRepository.AddRangeAsync(bots);
            await _cardHelper.CreateDecksAsync(model.NumberOfBots, game);
            await _cardHelper.CardsDeal(game, user, bots);

            //await CreateDeck(model.NumberOfBots + 1);


        }
    }
}
