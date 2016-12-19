using DataAccessLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Schedule;
using WebApplication1.Models;
using WebApplication1.Models.Event;

namespace WebApplication1.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public ActionResult Index(int eventId)
        {
            var db = new ApplicationDbContext();

            var e = db.Event.Find(eventId);

            if (e == null)
                return RedirectToAction("ShowSchedule", "Schedule");

            var eventData = new EventControlsViewModel();

            eventData.EventId = eventId;
            eventData.RoomId = e.ClassroomId;

            return View(eventData);
        }

        [HttpGet]
        public ActionResult GetDisplayEventPartial(int Id)
        {
            var db = new ApplicationDbContext();

            var eventEntity = db.Event.Where(e => e.Id == Id).FirstOrDefault();

            if (eventEntity == null)
                throw new NullReferenceException("There's no event with specified id!");


            bool isAuthorized = false;
            bool isAdmin = User.IsInRole("admin");
            string userId = User.Identity.GetUserId();

            if (isAdmin || eventEntity.ApplicationUserID == userId)
                isAuthorized = true;

            if (!eventEntity.IsPublic && !isAuthorized)
                throw new AccessViolationException("Not enough rights to view the private event");

            DisplayEventPartialViewModel model = new DisplayEventPartialViewModel();

            model.Id = eventEntity.Id;
            model.CanEdit = isAuthorized;
            model.AllowSubscription = (bool)eventEntity.AllowSubscription;
            model.DateStart = eventEntity.DateStart;
            model.DateEnd = eventEntity.DateEnd;
            model.Title = eventEntity.Title;
            model.Description = eventEntity.Description;

            if (eventEntity.OrganizerName != null)
                model.OrganizerName = eventEntity.OrganizerName;
            else
                model.OrganizerName = (eventEntity.Organizer == null) ? "" : eventEntity.Organizer.UserName;

            model.VisitorCount = eventEntity.ForeignVisitor.Count;

            return PartialView("_DisplayEventPartial", model);
        }

        [HttpGet]
        public PartialViewResult GetEventEditPartial(int eventId)
        {
            var db = new ApplicationDbContext();

            Event eventEntity = db.Event.Find(eventId);

            if (eventEntity == null)
                throw new NullReferenceException("Event with specified id does not exist");

            EditEventPartialViewModel viewModel;
            viewModel = new EditEventPartialViewModel();

            viewModel.AllowSubscription = (bool)eventEntity.AllowSubscription;
            viewModel.Description = eventEntity.Description;
            viewModel.End = eventEntity.DateEnd;
            viewModel.Start = eventEntity.DateStart;
            viewModel.Title = eventEntity.Title;
            viewModel.IsPublic = eventEntity.IsPublic;
            viewModel.OrganizerName = eventEntity.OrganizerName ?? eventEntity.Organizer?.UserName;

            viewModel.ShowAuthor = (eventEntity.OrganizerName == null);

            List<SelectListItem> items = new List<SelectListItem>();

            IEnumerable<ClassRoom> availRooms = db.ClassRoom.Where(r => r.IsBookable == true);

            int roomId = eventEntity.ClassroomId;

            foreach (var room in availRooms)
            {
                bool sel = room.Id == roomId;

                items.Add(new SelectListItem
                {
                    Text = room.Title,
                    Value = room.Id.ToString(),
                    Selected = sel
                }
                );
            }
            ViewBag.RoomIdOptions = items;
            // Pass id this way - will be replaced later
            ViewBag.eventId = eventId;
            ViewBag.CallBackAction = "EditEvent";
            ViewBag.IsNew = false;

            return PartialView("~/Views/Schedule/Overlays/EditEventPartialView.cshtml", viewModel);
        }


        [HttpGet]
        public ActionResult GetEventSubscribers(int eventId)
        {
            var db = new ApplicationDbContext();

            var eventEntity = db.Event.Find(eventId);

            if (eventEntity == null)
                throw new NullReferenceException("There's no event with specified id!");

            bool isAdmin = User.IsInRole("admin");
            string userId = User.Identity.GetUserId();

            if (!eventEntity.IsPublic && !(isAdmin || userId == eventEntity.ApplicationUserID))
                throw new AccessViolationException("Not enough rights to access a private event");

            List<ForeignVisitorViewModel> visitors = new List<ForeignVisitorViewModel>();

            var visitorEntities = eventEntity.ForeignVisitor.ToList();

            foreach (var visitor in visitorEntities)
            {
                visitors.Add(new ForeignVisitorViewModel { Id = visitor.Id, Email = visitor.Email});
            }

            return Json(new {visitors}, JsonRequestBehavior.AllowGet);
        }

        public void RemoveEventSubscriber(long eventId, long subId)
        {
            var db = new ApplicationDbContext();

            var eventEntity = db.Event.Find(eventId);

            if (eventEntity == null)
                throw new NullReferenceException("There's no event with specified id!");

            bool isAdmin = User.IsInRole("admin");
            string userId = User.Identity.GetUserId();

            if (!(isAdmin || userId == eventEntity.ApplicationUserID))
                throw new AccessViolationException("Not enough rights to edit event subscribers");

            var subToRemove = eventEntity.ForeignVisitor.Where(fv => fv.Id == subId).FirstOrDefault();

            if (subToRemove == null)
                throw new NullReferenceException("There's no subscriber with the specified id!");

            db.ForeignVisitor.Remove(subToRemove);
            db.SaveChanges();
        }


    }
}