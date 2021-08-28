using AutoMapper;
using AutoShop.Domain;
using AutoShop.Domain.Entities;
using AutoShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AppEFContext _context; /*Тут данні з БД чі для БД*/
        private User user = new User();
        //використовуємо авто мепер
        private readonly IMapper _mapper;
        public UserController(AppEFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProfilePage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ExitFromAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProfilePage(ProfilePageViewModel profilePage)
        {
            string UserName = HttpContext.Request.Cookies["UserPass"];
            string UserLogin = HttpContext.Request.Cookies["UserLogin"];
            profilePage.username = UserName;
            profilePage.userpassword = UserLogin;
            return View(profilePage);
        }
        [HttpPost]
        public IActionResult Registration(UserCreateViewModel UserCreate)
        {
            if (!ModelState.IsValid)
                return View(UserCreate);
            User user = new User
            {
                username = UserCreate.username,
                userpassword = UserCreate.userpassword,
                useremail = UserCreate.useremail
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Authorization(UserAuthorizationViewModel UserAuth)
        {
            string Pass = null;
            string Login = null;
            if (!ModelState.IsValid)
                return View(UserAuth);
            var query = _context.Users.AsQueryable();
            query = query.Where(x => x.username == UserAuth.username);
            if (query != null)
            {
                foreach (var item in query)
                {
                    Pass = item.userpassword;
                    Login = item.username;
                }
                if (Pass != null && Login != null && Pass == UserAuth.userpassword && Login == UserAuth.username)
                {
                    UserAuth.IsAuth = true;
                    HttpContext.Response.Cookies.Append("IsAuth", "True");
                    HttpContext.Response.Cookies.Append("UserPass", Pass);
                    HttpContext.Response.Cookies.Append("UserLogin", Login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(UserAuth);
                }
            }
            else
            {
                return View(UserAuth);
            }
        }
    }
}
