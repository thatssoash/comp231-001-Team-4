using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        private RoleManager<IdentityRole> roleManager;
        private IRepository repository;
        private int PageSize = 10;

        public AdminController(UserManager<AppUser> usrMgr,
                IUserValidator<AppUser> userValid,
                IPasswordValidator<AppUser> passValid,
                IPasswordHasher<AppUser> passwordHash,
                RoleManager<IdentityRole> roleMgr,
                IRepository reps)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            repository = reps;
            roleManager = roleMgr;
        }

        public async Task<IActionResult> UserList(string roleName, string txtTitle, int itemPage = 1)
        {
            roleName = roleName ?? "All";
            var usersOfRole = roleName != "All"
                            ? await userManager.GetUsersInRoleAsync(roleName)
                            : await userManager.Users.ToListAsync();
            txtTitle = txtTitle == null ? "" : txtTitle.ToUpper();
            UserViewmodel userList = new UserViewmodel
            {
                AppUsers = usersOfRole
                      .Where(r => r.UserName.ToUpper().Contains(txtTitle))
                      .OrderBy(u => u.UserName)
                      .Skip((itemPage - 1) * PageSize)
                      .Take(PageSize),
                Roles = roleManager.Roles
                   .OrderByDescending(r => r.Name)
                   .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = itemPage,
                    ItemsPerPage = PageSize,
                    TotalItems = userManager.Users.Count()
                }
            };
            foreach (var appUser in userList.AppUsers)
            {
                appUser.BusinessCount = repository.Businesses.Count(c => c.AppUser_Id == appUser.Id);
            }

            return View(userList);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User userModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    PhoneNumber = userModel.PhoneNumber,
                    Address = userModel.Address,
                    SIN = userModel.SIN
                };
                IdentityResult result
                    = await userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, "Client");
                    return RedirectToAction("UserList");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (IsAppuserInBusiness(id))
                {
                    ModelState.AddModelError("", "The User is connected to business, You cannot delete the user");
                    TempData["error"] = "The User is connected to business, You cannot delete the user";
                }
                else
                {
                    IdentityResult result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["message"] = "User has been deleted!";
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return RedirectToAction("UserList");
        }

        public bool IsAppuserInBusiness(string id)
        {

            Business bus = repository.Businesses.FirstOrDefault(a => a.AppUser_Id == id);
            if (bus == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("UserList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string username, string email,
                string password, string address, string phonenumber, string sin)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = username;
                user.Email = email;
                user.PhoneNumber = phonenumber;
                user.Address = address;
                user.SIN = sin;
                IdentityResult validEmail
                    = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager,
                        user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,
                            password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                        || (validEmail.Succeeded
                        && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
