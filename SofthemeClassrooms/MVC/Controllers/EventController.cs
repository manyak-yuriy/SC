using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public ActionResult Index(int id)
        {
            return new EmptyResult();
        }

        // Returns a partial view - overlay with event info
        [HttpGet]
        public ActionResult EventInfoOverlay(int Id)
        {
            return new EmptyResult();
        }

        // Returns a partial view - overlay with event edit option
        [HttpGet]
        public ActionResult EventEditOverlay(int Id)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Add(EventEditViewModel eventInfo)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Modify(EventEditViewModel eventInfo)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Subscribe(string eMail)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult CancelSubscription(string eMail)
        {
            return new EmptyResult();
        }
    }
}