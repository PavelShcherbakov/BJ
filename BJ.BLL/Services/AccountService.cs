using BJ.BLL.Exceptions;
using BJ.BLL.Providers.Interfaces;
using BJ.BLL.Services.Interfaces;
using BJ.Entities;
using BJ.ViewModels.AccountViews;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _jwtTokentHelper;


        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenProvider jwtTokentHelper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokentHelper = jwtTokentHelper;
        }

        public async Task<LoginAccountResponseView> Login(LoginAccountView model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                throw new CustomServiceException("Invalid login attempt");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = _jwtTokentHelper.GenerateJwtToken(model.Email, user);
            var response = new LoginAccountResponseView()
            {
                Token = token
            };
            return response;
        }


        public async Task<LoginAccountResponseView> Register(RegisterAccountView model)
        {

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new CustomServiceException("Invalid user");
            }


            var token = _jwtTokentHelper.GenerateJwtToken(model.Email, user);
            var response = new LoginAccountResponseView()
            {
                Token = token
            };
            return response;

        }

        public async Task<GetAllUserAccountResponseView> GetAllUsers()
        {
            var userNameList = new List<string>();
            var names = _userManager.Users.ToList().Select(x => x.UserName);
            userNameList.AddRange(names);

            var response = new GetAllUserAccountResponseView()
            {
                UserNames = userNameList
            };
            return response;
        }


    }
}
