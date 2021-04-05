using AccountApp.BOL;
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
    [Authorize(Roles = "Accountant")]

    public class ManagerController : Controller
    {
        private IRepository repository;
        private int PageSize = 10;
        private UserManager<AppUser> userManager;


        public ManagerController(IRepository repo, UserManager<AppUser> userMng)
        {
            repository = repo;
            userManager = userMng;
        }

        // -- Business ------------------
        public async Task<IActionResult> Business(string txtTitle, string Id, int itemPage = 1)
        {
            txtTitle = txtTitle == null ? "" : txtTitle.ToUpper();
            Id = Id ?? "";
            TempData["Username"] = Id == "" ? "" : GetUsername(Id);
            return View(new BusinessVM
            {
                Businesses = repository.Businesses
                     .Where(r => r.Title.ToUpper().Contains(txtTitle) &&
                                 r.AppUser_Id.Contains(Id))
                     .OrderBy(r => r.Title)
                     .Skip((itemPage - 1) * PageSize)
                     .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = itemPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Businesses.Count()
                },
                AppUser = await userManager.FindByIdAsync(Id)
            });
        }

        public ViewResult EditBusiness(int ID)
        {
            string userName = GetUsername(GetBizUserId(ID));
            TempData["Username"] = userName;
            return View(repository.Businesses
                .FirstOrDefault(f => f.ID == ID));
        }

        [HttpPost]
        public IActionResult EditBusiness(Business biz)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBiz(biz);
                TempData["message"] = $"Business {biz.Title} has been saved successfully!";
                string userId = biz.AppUser_Id;
                BusinessVM bizVM = new BusinessVM
                {
                    Businesses = repository.Businesses
                            .Where(r => r.AppUser_Id == biz.AppUser_Id).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = 1,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Businesses
                                .Count(c => c.AppUser_Id == biz.AppUser_Id)
                    },
                    AppUser = userManager.Users
                            .FirstOrDefault(u => u.Id == biz.AppUser_Id)
                };
                return View("Business", bizVM);
            }
            else
            {
                return View(biz);
            }
        }

        public ViewResult CreateBusiness(string Id)
        {
            Business biz = new Business
            {
                AppUser_Id = Id
            };
            TempData["Username"] = GetUsername(Id);
            ModelState.Clear();
            return View("EditBusiness", biz);
        }

        [HttpPost]
        public IActionResult DeleteBusiness(int ID)
        {
            // find business in clientforms .....
            //

            Business deleteRecord = repository.DeleteBiz(ID);

            if (deleteRecord != null)
            {
                TempData["message"] = $" Business {deleteRecord.Title} has been deleted!";
                string userId = deleteRecord.AppUser_Id;
                BusinessVM bizVM = new BusinessVM
                {
                    Businesses = repository.Businesses
                            .Where(r => r.AppUser_Id == deleteRecord.AppUser_Id).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = 1,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Businesses
                                .Count(c => c.AppUser_Id == deleteRecord.AppUser_Id)
                    },
                    AppUser = userManager.Users
                            .FirstOrDefault(u => u.Id == deleteRecord.AppUser_Id)
                };
                return View("Business", bizVM);
            }
            else
            {
                return RedirectToAction("Business");
            }

        }

        // -- Form ------------------
        public ViewResult Forms()
        {
            return View(repository.Forms);
        }

        public ViewResult EditForm(int ID) =>
            View(repository.Forms
                .FirstOrDefault(f => f.ID == ID));

        [HttpPost]
        public IActionResult EditForm(Form form)
        {
            if (ModelState.IsValid)
            {
                repository.SaveForm(form);
                TempData["message"] = $"Form {form.FormCode} has been saved successfully!";
                return RedirectToAction("Forms");
            }
            else
            {
                return View(form);
            }
        }

        public ViewResult CreateForm() => View("EditForm", new Form());

        [HttpPost]
        public IActionResult DeleteForm(int ID)
        {
            FormDetail frmd = repository.FormDetails.FirstOrDefault(f => f.FormId == ID);
            if (frmd != null)
            {
                TempData["error"] = "This Form has Subform Details, you cannot delete it.";
            }
            else
            {
                Form deleteRecord = repository.DeleteForm(ID);
                if (deleteRecord != null)
                {
                    TempData["message"] = $" Form {deleteRecord.FormCode} has been deleted!";
                }
            }
            return RedirectToAction("Forms");
        }


        // -- Form Detail ------------------
        public ViewResult Formdetails(int ID)
        {
            return View(repository.FormDetails.Where(r => r.FormId == ID).ToList());
        }

        public ViewResult EditFd(int ID) =>
            View(repository.FormDetails
                .FirstOrDefault(f => f.ID == ID));

        [HttpPost]
        public IActionResult EditFd(FormDetail formDetail)
        {
            if (ModelState.IsValid)
            {
                var frmId = formDetail.FormId;
                repository.SaveFormDetail(formDetail);
                TempData["message"] = $"The row of {formDetail.AccCode} has been saved!";
                var frmDetails = repository.FormDetails.Where(r => r.FormId == frmId).ToList();
                return View("FormDetails", frmDetails);
            }
            else
            {
                return View(formDetail);
            }
        }

        public ViewResult CreateFd() => View("EditFd", new FormDetail());

        [HttpPost]
        public IActionResult DeleteFd(int ID)
        {
            // - Find AccCode in Client-subform details ----------
            //

            FormDetail deleteRecord = repository.DeleteFormDetail(ID);

            if (deleteRecord != null)
            {
                TempData["message"] = $"The row {deleteRecord.AccCode} has been deleted!";
            }

            return RedirectToAction("FormDetails");
        }

        // ---------------------------------------------------
        public string GetBizUserId(int bizID)
        {
            Business busi = repository.Businesses.FirstOrDefault(u => u.ID == bizID);
            return busi.AppUser_Id;
        }
        public string GetUsername(string userId)
        {
            AppUser user = userManager.Users.FirstOrDefault(r => r.Id == userId);
            return user.UserName;
        }


    }
}
