using DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using WebApplication1.Models.Schedule;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Render the page
        [HttpGet]
        public ActionResult ShowSchedule()
        {           
            var eq = new Equipment { Title = "TV", ImagePath = "F:" };
            db.Equipment.AddOrUpdate(e => e.Title, eq);

            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            var classRoom = new ClassRoom { Title = "NewTon", Capacity = 45, IsBookable = true};

            db.ClassRoom.AddOrUpdate(c => c.Title, classRoom);

            var startsAt = new DateTime(2016, 12, 08) + new TimeSpan(9, 15, 0);
            var endsAt = new DateTime(2016, 12, 08) + new TimeSpan(11, 10, 0);

            var ev = new Event { ClassroomId = classRoom.Id, AllowSubscription = true, ApplicationUserID = User.Identity.GetUserId(), DateStart = startsAt, DateEnd = endsAt, Title = "ASP.NET 2nd lecture", Description = "Cool event", IsPublic = true};

            db.Event.AddOrUpdate(e => e.Title, ev);
            
            var fb = db.Feedback.ToList();
            db.SaveChanges();
            
            return View();
        }

        public PartialViewResult GetDisplayEventPartial(int Id)
        {
            var eventEntity = db.Event.Where(e => e.Id == Id).First();

            if (eventEntity == null)
                throw new NullReferenceException("There's no event with specified id!");

            DisplayEventPartialViewModel model = new DisplayEventPartialViewModel();
            model.DateStart = eventEntity.DateStart;
            model.DateEnd = eventEntity.DateEnd;
            model.Title = eventEntity.Title;
            model.Description = eventEntity.Description;
            if (eventEntity.OrganizerName != null)
                model.OrganizerName = eventEntity.OrganizerName;
            else
                model.OrganizerName = (eventEntity.Organizer == null)? "" : eventEntity.Organizer.UserName;

            model.VisitorCount = eventEntity.ForeignVisitor.Count;

            return PartialView("~/Views/Schedule/Overlays/DisplayEventPartialView.cshtml", model);
        }

        [HttpGet]
        public JsonResult GetEventDataForDay(DateTime daySelected)
        {
            var db = new ApplicationDbContext();
            var eventsData = db.Event.Where(e => e.DateStart.Day == daySelected.Day)
                .Select(e => new { e.Id, e.DateStart, e.DateEnd, e.ClassroomId, classRoomTitle = e.ClassRoom.Title, e.Title, e.IsPublic});
            var roomData = eventsData.Select(e => new { e.ClassroomId, e.classRoomTitle } ).Distinct();
            return Json(new { roomData, eventsData } , JsonRequestBehavior.AllowGet);
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