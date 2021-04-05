using AccountApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userMgr,
            SignInManager<AppUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    if ((await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public ViewResult Forgot()
        {
            return View(new Forgot { Email = null });
        }

        [HttpPost]
        public async Task<IActionResult> Forgot(string Email)
        {
            Forgot forg = new Forgot();

            if (Email != null)
            {
                AppUser user = await userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    // send email to client
                    // Utility.SendEmail(Email, "", "");
                    TempData["message"] = $"A Reset link has been sending to {Email}";
                    forg.Email = Email;
                }
                else
                {
                    ModelState.AddModelError("", "Email not exists.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email.");
            }
            return View(forg);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
