using BJ.BLL.Services;
using BJ.ViewModels.HistoryView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class HistoryController : BaseController
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            return await Execute(() => _historyService.GetAllGames(UserId));
        }
        [HttpPost]
        public async Task<IActionResult> GetGameInfo(GetGameInfoHistoryView model)
        {
            return await Execute(() => _historyService.GetGameInfo(UserId, model));
        }
    }
}