using DataAccessLayer;
using ManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {
        // Get equipment data displayed on the panel
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetEquipmentDataForRoom(int roomId)
        {
            ManagementServices.Implementations.EquipmentManager equipmentManager = new ManagementServices.Implementations.EquipmentManager();
            var equipmentData = equipmentManager.GetEquipmentByRoomId(roomId);
            return Json(equipmentData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetEquipmentDataForRoom(int roomId, string roomTitle, EquipmentViewModel equipmentData)
        {
            
            if (!User.IsInRole("admin"))
                return new HttpUnauthorizedResult();
                
            ManagementServices.Implementations.EquipmentManager equipmentManager = new ManagementServices.Implementations.EquipmentManager();
            
            equipmentManager.SetEquipmentByRoomId(roomId, roomTitle, equipmentData);
            
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Close(int roomId)
        {
            if (!User.IsInRole("admin"))
                return new HttpUnauthorizedResult();

            var db = new ApplicationDbContext();

            var room = db.ClassRoom.Find(roomId);

            if (room != null)
            {
                room.IsBookable = false;

                var eventsToDelete = room.Event.Where(e => e.ClassroomId == roomId).ToList();

                foreach (var e in eventsToDelete)
                {
                    var fvToDelete = db.ForeignVisitor.Where(fv => fv.EventId == e.Id).ToList();
                    db.ForeignVisitor.RemoveRange(fvToDelete);
                }

                db.Event.RemoveRange(eventsToDelete);
            }

            db.SaveChanges();

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Open(int roomId)
        {
            var db = new ApplicationDbContext();

            var room = db.ClassRoom.Find(roomId);

            if (room != null)
            {
                room.IsBookable = true;
            }

            db.SaveChanges();

            return new EmptyResult();
        }
              
        [HttpGet]
        public ActionResult Index(int roomId)
        {
            var db = new ApplicationDbContext();

            ViewBag.roomId = roomId;
            var room = db.ClassRoom.Find(roomId);

            if (room != null)
            {
                ViewBag.isBookable = room.IsBookable;
                ViewBag.Title = room.Title;
                return View();
            }
            else
                throw new ArgumentException("No room with specified id exists!");
            
        }
    }
}