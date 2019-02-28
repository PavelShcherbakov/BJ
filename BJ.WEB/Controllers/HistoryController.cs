using BJ.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BJ.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : BaseController
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }
    }
}