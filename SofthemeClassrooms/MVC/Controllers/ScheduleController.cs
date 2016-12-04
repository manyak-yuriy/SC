using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        // Render the page
        [HttpGet]
        public ActionResult ShowSchedule()
        {
            var db = new ApplicationDbContext();
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public JsonResult GetEquipmentDataForRoom(int roomId)
        {
            ManagementServices.Implementations.EquipmentManagement equipmentManager = new ManagementServices.Implementations.EquipmentManagement();
            return Json(equipmentManager.GetEquipmentByRoomId(roomId), JsonRequestBehavior.AllowGet);
        }

        // Get data about events for a specific date
        [HttpGet]
        public ActionResult GetSchedulerState(DateTime date)
        {
            return new EmptyResult();
        }

        // Get equipment info for a specific room
        [HttpGet]
        public ActionResult GetRoomInfo(int roomId)
        {
            return new EmptyResult();
        }
    }
}