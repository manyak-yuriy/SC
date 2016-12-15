using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorHandlerController : Controller
    {
        // GET: Error
        public ActionResult ErrorOccurred()
        {
            return View();
        }
    }
}