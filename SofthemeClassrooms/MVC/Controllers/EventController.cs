using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public ActionResult Index(int eventId)
        {
            return View();
        }

    }
}