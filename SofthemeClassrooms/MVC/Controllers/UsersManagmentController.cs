using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UsersManagmentController : Controller
    {
        public ActionResult Users()
        {
            return View();
        }
    }
}