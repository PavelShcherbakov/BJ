using BJ.BLL.Commons;
using BJ.BLL.Exceptions;
using BJ.BLL.Extensions;
using BJ.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BJ.WEB.Controllers
{
    public class BaseController:Controller
    {
        public string UserId
        {
            get
            {
                return User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            }
        }
    }
}
