using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class ProfileController : Controller
    {
        [HttpPost]
        public ActionResult Remove(int id)
        {
            return new EmptyResult();
        }

        // Return a partial view for modification
        [HttpGet]
        public ActionResult Modify(int id)
        {
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult GetSchedulerState(int id, DateTime date)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Save(ProfileChangeViewModel profileData)
        {
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            return new EmptyResult();
        }
    }
}