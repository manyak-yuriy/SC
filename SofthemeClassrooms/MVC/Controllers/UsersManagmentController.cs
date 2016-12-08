using System.Web.Mvc;
using ManagementServices.Implementations;
using WebApplication1.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Security.Claims;

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

        public void SaveUserDataToSession(string email)
        {
            if ((Session["UserId"] as string) != null)
                return;

            var user = UserManager.FindByEmail(email);

            Session["UserId"] = user.Id;
            Session["UserEmail"] = user.Email;

            Session["UserName"] = UserManager.GetClaims(user.Id).Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value).SingleOrDefault();

        }

        public ActionResult MyPage()
        {
            SaveUserDataToSession(User.Identity.Name);
            return View();
        }

        public ActionResult ChangePersonalView()
        {
            return PartialView("ChangePersonalDataPView", new PersonalDataViewModel() { Email = Session["UserEmail"] as string, Name = Session["UserName"] as string });
        }

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

            ///Here Should be code to change personal data

            return PartialView("PersonalInfoPView");
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

        [Authorize(Roles ="admin")]
        public ActionResult Users()
        {
            AppUsersManager m = new AppUsersManager();
            PersonalDataViewModel pView = new PersonalDataViewModel();
            var users = pView.CreateFromUserInfo(m.GetUsersInfo());
            return View(users);
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
        public ActionResult DeleteUser(string email)
        {
            return new EmptyResult();
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