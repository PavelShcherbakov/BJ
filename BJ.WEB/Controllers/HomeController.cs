using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BJ.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using BJ.BLL.Configrutions;

namespace BJ.WEB.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index([FromServices]IOptions<DBOptions> settings)
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
