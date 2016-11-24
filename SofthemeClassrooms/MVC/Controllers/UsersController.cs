using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public ActionResult Search(string keyword)
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