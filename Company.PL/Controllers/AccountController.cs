﻿using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };
                var Result =  await _userManager.CreateAsync(User , model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                    foreach(var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description); 
                 
            }
            return View(model);
        }
	}
}
