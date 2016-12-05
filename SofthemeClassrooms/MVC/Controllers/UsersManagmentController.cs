using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UsersManagmentController : Controller
    {
        // GET: UsersManagment
        public ActionResult Users()
        {
            return View();
        }
    }
}