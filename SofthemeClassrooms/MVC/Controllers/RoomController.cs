using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {
        [HttpPost]
        public ActionResult Close(int roomId)
        {
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
                ViewBag.isBookable = room.IsBookable;

            return View();
        }
    }
}