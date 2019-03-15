using BJ.BLL.Services.Interfaces;
using BJ.ViewModels;
using BJ.ViewModels.AccountViews;
using BJ.WEB.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;

namespace BJ.WEB.Controllers
{
    
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost]
        public async Task<GenericResponseView<LoginAccountResponseView>> Login([FromBody] LoginAccountView model)
        {
            var response = new GenericResponseView<LoginAccountResponseView>();
            response.Model = await _accountService.Login(model);
            return response;
        }

        [HttpPost]
        public async Task<GenericResponseView<LoginAccountResponseView>> Register([FromBody] RegisterAccountView model)
        {
            var response = new GenericResponseView<LoginAccountResponseView>();
            response.Model = await _accountService.Register(model);
            return response;
        }

        [HttpGet]
        public async Task<GenericResponseView<GetAllUserAccountResponseView>> GetAllUsers()
        {
            var response = new GenericResponseView<GetAllUserAccountResponseView>();
            response.Model = await _accountService.GetAllUsers();
            return response;
        }
    }
}