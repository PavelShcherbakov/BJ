using BJ.BLL.Services;
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
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> Start([FromBody] StartGameView model)
        {
            return await Execute(() => _gameService.StartGame(UserId, model)); 
        }

        [HttpPost]
        public async Task<IActionResult> GetCard([FromBody]GetCardGameView model)
        {
            return await Execute(() => _gameService.GetCard(UserId, model));
        }

        [HttpPost]
        public async Task<IActionResult> End([FromBody]EndGameView model)
        {
            return await Execute(() => _gameService.EndGame(UserId, model));
        }
    }
}