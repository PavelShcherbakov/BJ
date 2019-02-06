using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BJ.ViewModels.AccountViews;
using BJ.BLL.Services;

namespace BJ.WEB.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly AccountService _accountService;
        

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginAccountView model)
        {
            return await Execute(()=>_accountService.Login(model));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAccountView model)
        {
            return await Execute(() => _accountService.Register(model));
        }  
    }
}