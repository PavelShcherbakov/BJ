using BJ.ViewModels.AccountViews;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BJ.BLL.Helpers;
using BJ.BLL.Exceptions;
using BJ.Entities;
using System.Collections.Generic;

namespace BJ.BLL.Services
{
    public class AccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenHelper _jwtTokentHelper;


        public AccountService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            JwtTokenHelper jwtTokentHelper
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

            var user = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
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

            await _signInManager.SignInAsync(user, false);
            var token = _jwtTokentHelper.GenerateJwtToken(model.Email, user);
            var response = new LoginAccountResponseView()
            {
                Token = token
            };
            return response;
  
        }

        public async Task<GetAllUserAccountResponseView> GetAllUsers()
        {
            List<string> userNameList = new List<string>();
            _userManager.Users.ToList().ForEach(x=> userNameList.Add(x.UserName));

            GetAllUserAccountResponseView response = new GetAllUserAccountResponseView() { UserNames = userNameList };
            return response;
        }


    }
}
