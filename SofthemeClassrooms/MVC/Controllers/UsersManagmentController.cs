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