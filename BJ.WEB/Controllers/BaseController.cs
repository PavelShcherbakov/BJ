using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BJ.WEB.Controllers
{
    public class BaseController : Controller
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
