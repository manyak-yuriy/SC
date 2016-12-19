using DataAccessLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Schedule;

namespace WebApplication1.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public ActionResult Index(int eventId)
        {
            return View(eventId);
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

    }
}