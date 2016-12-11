using System.Web.Mvc;
using ManagementServices.Implementations;
using ManagementServices.Models;
using WebApplication1.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Security.Claims;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UsersManagmentController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }



        public ActionResult MyPage()
        {
            var user = Session["User"] as PersonalDataViewModel;
            if (user == null)
            {
                AppUsersManager uManager = new AppUsersManager();
                var uInfo = uManager.GetUserInfo(User.Identity.Name);
                user = PersonalDataViewModel.CreateFromUserInfo(uInfo);
                Session["User"] = user;
            }

            return View(user);
        }

        public ActionResult ChangePersonalView()
        {
            return PartialView("ChangePersonalDataPView", new PersonalDataViewModel() { Email = Session["UserEmail"] as string, Name = Session["UserName"] as string });
        }

        [HttpPost]
        public ActionResult ChangePersonalInfo(PersonalDataViewModel model)
        {
            if (model.Email == null || model.Name == null)
            {
                return PartialView("ChangePersonalDataPView", new PersonalDataViewModel() { Email = Session["UserEmail"] as string, Name = Session["UserName"] as string });
            }

            if (!ModelState.IsValid)
            {
                return PartialView("ChangePersonalDataPView", model);
            }

            AppUsersManager manager = new AppUsersManager();
            manager.UpdateUser(model.ToUserInfo());

            return PartialView("PersonalInfoPView");
        }

        public ActionResult ChangePasswordView()
        {
            return PartialView("ChangePasswordPartialView");
        }

        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("ChangePasswordPartialView", model);
            }

            var user = UserManager.FindByName(User.Identity.Name);
            var result = UserManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                model.ConfirmPassword = "";
                model.NewPassword = "";
                model.OldPassword = "";
                return PartialView("ChangePasswordPartialView", model);
            }

            AddErrors(result);
            return PartialView("ChangePasswordPartialView", model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Users(int page = 0)
        {
            if (page < 0)
            {
                return new HttpNotFoundResult();
            }

            AppUsersManager m = new AppUsersManager();
            int itemsPerPage = 20,
                NumberOfUsers = m.GetUserNumber();
            int lastPage = NumberOfUsers / itemsPerPage;
            if (lastPage < page)
            {
                return new HttpNotFoundResult();
            }

            var usersInfo = m.GetUsersInfo(page, itemsPerPage);
            var users = PersonalDataViewModel.CreateFromUsersInfo(usersInfo);
            PageInfo pageInfo = new PageInfo
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                TotalNumOfItems = NumberOfUsers
            };

            return View(new Pair<PageInfo, IEnumerable<PersonalDataViewModel>>
            {
                item1 = pageInfo,
                item2 = users
            });
        }

        public ActionResult Search(string pattern)
        {
            

            return View();
        }

        public ActionResult ChangeUserInfoView(PersonalDataViewModel model)
        {
            return PartialView(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult UserPage(PersonalDataViewModel model)
        {
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteUser(string Id)
        {
            if (User.Identity.GetUserId() != Id)
            {
                AppUsersManager manager = new AppUsersManager();
                manager.DeleteUser(Id);
            }

            return new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"controller", "Schedule"},
                    {"action", "ShowSchedule" }
                }); ;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}