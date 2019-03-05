using BJ.ViewModels.AccountViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BJ.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginAccountResponseView> Login(LoginAccountView model);

        Task<LoginAccountResponseView> Register(RegisterAccountView model);

        Task<GetAllUserAccountResponseView> GetAllUsers();
    }
}
