using AutoShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(UserAuthorizationViewModel userAuthorization)
        {
            string auth = HttpContext.Request.Cookies["IsAuth"];
            if (auth == "True")
            {
                string Pass = HttpContext.Request.Cookies["UserPass"];
                string Login = HttpContext.Request.Cookies["UserLogin"];

                if (Pass != null && Login != null)
                {
                    userAuthorization.IsAuth = true;
                    userAuthorization.username = Login;
                }
            }
            return View();
        }

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
