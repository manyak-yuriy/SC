using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpPost]
        public ActionResult Index(FeedbackViewModel feedback)
        {
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return new EmptyResult();
        }
    }
}