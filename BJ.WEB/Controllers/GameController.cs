using BJ.BLL.Services.Interfaces;
using BJ.ViewModels;
using BJ.ViewModels.GameViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace BJ.WEB.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<GenericResponseView<StartGameResponseView>> Start([FromBody] StartGameView model)
        {
            var response = new GenericResponseView<StartGameResponseView>();
            response.Model = await _gameService.StartGame(UserId, model);
            return response;
        }

        [HttpPost]
        public async Task<GenericResponseView<GetCardGameResponseView>> GetCard()
        {
            var response = new GenericResponseView<GetCardGameResponseView>();
            response.Model = await _gameService.GetCard(UserId);
            return response;
        }

        [HttpGet]
        public async Task<GenericResponseView<HasActiveGameGameResponseView>> HasActiveGame()
        {
            var response = new GenericResponseView<HasActiveGameGameResponseView>();
            response.Model = await _gameService.HasActiveGame(UserId);
            return response;
        }

        [HttpGet]
        public async Task<GenericResponseView<GetStateGameResponseView>> GetState()
        {
            var response = new GenericResponseView<GetStateGameResponseView>();
            response.Model = await _gameService.GetState(UserId);
            return response;
        }

        [HttpPost]
        public async Task<GenericResponseView<EndGameResponseView>> End()
        {
            var response = new GenericResponseView<EndGameResponseView>();
            response.Model = await _gameService.EndGame(UserId);
            return response;
        }
    }
}