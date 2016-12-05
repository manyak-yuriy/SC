using System.Web.Mvc;
using ManagementServices.Implementations;

namespace WebApplication1.Controllers
{
    public class UsersManagmentController : Controller
    {
        public ActionResult Users()
        {
            UsersManager m = new UsersManager();
            var users = m.GetUsersInfo();
            
            return View();
        }
    }
}