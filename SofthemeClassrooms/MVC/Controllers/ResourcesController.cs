using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ResourcesController : Controller
    {
        // GET: Resources
        [HttpGet]
        public ActionResult GetImage(string imageName)
        {
            if(!string.IsNullOrEmpty(imageName))
            {
                string path = @"~/Images" + @"/" + imageName;
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    return File(file.FullName, "image/png", file.Name);
            }
            return Content("");
        }
    }
}