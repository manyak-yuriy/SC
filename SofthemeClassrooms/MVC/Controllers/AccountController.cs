using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication1.Models;
using DataAccessLayer;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }

        public ActionResult LoginForm()
        {
            return PartialView("LoginInput");
        }

        public ActionResult ChangePasswordForm()
        {
            return PartialView("ChangePasswordPartialView");
        }

        public ActionResult ForgotPassword()
        {
            return PartialView("ForgotPasswordPartialView");
        }

        public ActionResult RegisterForm()
        {
            return PartialView("RegistrationPartialView");
        }
    }
}