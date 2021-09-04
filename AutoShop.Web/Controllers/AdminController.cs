using AutoShop.Domain.Entities;
using AutoShop.Domain.Entities.Identity;
using AutoShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        UserManager<AppUser> _userManager;
        RoleManager<AppRole> _roleManager;
        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index() => View(_userManager.Users.ToList());
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Delete()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (model.Email == null || model.Password == null)
            {
                return View();
            }
            else
            {
                string[] nameUser = model.Email.Split(new char[] { '@' });
                AppUser user = new AppUser { Email = model.Email, UserName = nameUser[0]};
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, model.RoleSelect);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserViewModel model)
        {
            if (model.Id == null)
            {
                return View();
            }
            else
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                }
                return RedirectToAction("Index", "Admin");
            }
        }
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    string[] nameUser = model.Email.Split(new char[] { '@' });
                    user.Email = model.Email;
                    user.UserName = nameUser[0];
                    await _userManager.AddToRoleAsync(user, model.RoleSelect);
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            return View();
                        }
                    }
                }
            }
            return View();
        }
    }
}
