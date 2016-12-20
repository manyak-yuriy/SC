using System.Web.Mvc;
using ManagementServices.Implementations;
using ManagementServices.Models;
using WebApplication1.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ManagementServices.Interfaces;
using WebApplication1.Extensions;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UsersManagmentController : Controller
    {
        private readonly IBusinessLogicFactory _businessLogicFactory;

        public UsersManagmentController(IBusinessLogicFactory factory)
        {
            _businessLogicFactory = factory;
        }

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
                IUserManager uManager = _businessLogicFactory.UserManager;
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

            model.Email = model.Email.DeleteExtraSpaces();
            model.Name = model.Name.DeleteExtraSpaces();

            IUserManager manager = _businessLogicFactory.UserManager;
            manager.UpdateUser(model.ToUserInfo());

            if (model.Id == User.Identity.GetUserId())
            {
                Session["UserName"] = model.Name;
                Session["User"] = model;
            }

            return PartialView("PersonalInfoPView", model);
        }

        public ActionResult ChangePasswordView()
        {
            return PartialView("ChangePasswordPartialView");
        }

        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return PartialView("ChangePasswordPartialView", model);
            }

            var user = UserManager.FindByName(User.Identity.Name);
            var result = UserManager
                .ChangePassword(user.Id, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                model.ConfirmPassword = "";
                model.NewPassword = "";
                model.OldPassword = "";
                return PartialView("ChangePasswordPartialView", model);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            AddErrors(result);
            return PartialView("ChangePasswordPartialView", model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Users(string searcPattern, int page = 1)
        {
            if (page < 1)
            {
                return new HttpNotFoundResult();
            }

            IUserManager m = _businessLogicFactory.UserManager;
            int itemsPerPage = 20;
            int numberOfUsers = m.GetUserNumber();

            int lastPage = numberOfUsers / itemsPerPage;
            int remainder = numberOfUsers % itemsPerPage;
            lastPage += remainder > 0 ? 1 : 0;

            if (lastPage < page)
            {
                return new HttpNotFoundResult();
            }

            IEnumerable<UserInfo> usersInfo =
                string.IsNullOrEmpty(searcPattern) ?
                m.GetUsersInfo(page, itemsPerPage) :
                m.GetUsersInfo(page, itemsPerPage, searcPattern.DeleteExtraSpaces());


            var users = PersonalDataViewModel.CreateFromUsersInfo(usersInfo);

            PageInfo pageInfo = new PageInfo
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = page,
                TotalNumOfItems = numberOfUsers
            };

            UsersPageModel mod = new UsersPageModel
            {
                PageInfo = pageInfo,
                SearchPattern = searcPattern,
                Users = users
            };
            return View(mod);
        }


        [Authorize(Roles = "admin")]
        public ActionResult UserPage(PersonalDataViewModel model)
        {
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string Id)
        {
            if (User.Identity.GetUserId() != Id)
            {
                AppUsersManager manager = new AppUsersManager();
                await UserManager.SendEmailAsync(Id, "Удаление учетной записи.", "За решением администрации сайта Softheme Classroom Portal, ваш аккаунт удален.");
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