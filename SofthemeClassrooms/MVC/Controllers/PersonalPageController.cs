using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonalPageController : Controller
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}