using BJ.BLL.Services;
using BJ.BLL.Services.Interfaces;
using BJ.ViewModels;
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
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<GenericResponseView<GetAllGamesHistoryResponseView>> GetAllGames()
        {
            var response = new GenericResponseView<GetAllGamesHistoryResponseView>();
            response.Model = await _historyService.GetAllGames(UserId);
            return response;
        }
        [HttpPost]
        public async Task<GenericResponseView<GetGameInfoHistoryResponseView>> GetGameInfo([FromBody]GetGameInfoHistoryView model)
        {
            var response = new GenericResponseView<GetGameInfoHistoryResponseView>();
            response.Model = await _historyService.GetGameInfo(UserId, model);
            return response;
        }
    }
}