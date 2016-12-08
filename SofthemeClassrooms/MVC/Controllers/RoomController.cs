using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {
        // Save info about capacity, number of equipment items
        [HttpPost]
        public ActionResult SaveProperties(/*RoomPropViewModel roomProp*/)
        {
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Close(int roomId)
        {
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult Open(int roomId)
        {
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Edit(int roomId)
        {
            return new EmptyResult();
        }

        // Returns information about events
        [HttpGet]
        public ActionResult GetSchedulerState(int roomId, DateTime fromDate, DateTime toDate)
        {
            return new EmptyResult();
        }

        // Returns room capacity, number of items
        [HttpGet]
        public ActionResult GetProperties(int roomId)
        {
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Index(int roomId)
        {
            return View();
        }
    }
}