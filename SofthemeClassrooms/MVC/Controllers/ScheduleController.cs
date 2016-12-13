﻿using DataAccessLayer;
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

        // Render main page
        [HttpGet]
        public ActionResult ShowSchedule()
        {
            return View();
        }

        // Returns partial view with popup contents for displaying info for a particular event
        [HttpGet]
        public ActionResult GetDisplayEventPartial(int Id)
        {
            var eventEntity = db.Event.Where(e => e.Id == Id).First();

            if (eventEntity == null)
                throw new NullReferenceException("There's no event with specified id!");


            bool isAuthorized = false;
            bool isAdmin = User.IsInRole("admin");
            string userId = User.Identity.GetUserId();

            if (isAdmin || eventEntity.ApplicationUserID == userId)
                isAuthorized = true;

            if (!eventEntity.IsPublic && !isAuthorized)
                throw new AccessViolationException("Not enough rigths to view the private event");

                DisplayEventPartialViewModel model = new DisplayEventPartialViewModel();
            model.CanEdit = isAuthorized;
            model.AllowSubscription = (bool) eventEntity.AllowSubscription;
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
        public PartialViewResult GetEventCancellationPartialView(int Id)
        {
            var e = db.Event.Find(Id);
            return PartialView("~/Views/Schedule/Overlays/CancelEventPartialView.cshtml", e.Title);
        }

        [HttpGet]
        public PartialViewResult GetEventAddPartialView()
        {
            EditEventPartialViewModel viewModel;
            
            viewModel = new EditEventPartialViewModel();
            viewModel.Start = DateTime.Now;
            viewModel.End = DateTime.Now.AddHours(2);

            List<SelectListItem> items = new List<SelectListItem>();

            IEnumerable<ClassRoom> availRooms = db.ClassRoom.Where(r => r.IsBookable == true);

            foreach (var room in availRooms)
            {
                items.Add(new SelectListItem
                    {
                         Text = room.Title, Value = room.Id.ToString()
                    }
                );
            }
            ViewBag.RoomIdOptions = items;
            return PartialView("~/Views/Schedule/Overlays/AddEventPartialView.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult AddSubscriber(int eventId, NewSubscriberViewModel subModel)
        {
            var e = db.Event.Find(eventId);

            if (e == null)
                throw new NullReferenceException("No event with a given id exists");

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();

                return Json(new { result = "fail", errorList});
            }
                

            var newVis = new ForeignVisitor();

            newVis.Email = subModel.Email;
            newVis.Event = e;

            db.ForeignVisitor.Add(newVis);

            db.SaveChanges();

            return Json(new { result = "success"});
        }

        [HttpPost]
        public ActionResult AddNewEvent(EditEventPartialViewModel eventModel)
        {
            var classRoom = db.ClassRoom.Find(Int32.Parse(eventModel.RoomId));
            var errors = new ErrorModel();

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToList();

                foreach (string s in errorList)
                    errors.Errors.Add(s);
            }
                

            if (classRoom == null)
            {
                errors.Errors.Add("Specified room does not exist");
            }
            else if (!classRoom.IsBookable)
            {
                errors.Errors.Add("Specified room is not bookable");
            }
            else
            {
                var eventsForClassRoom = db.Event.Where(e => e.ClassroomId == classRoom.Id);

                DateTime startDate = eventModel.Start;
                DateTime endDate = eventModel.End;

                foreach (var e in eventsForClassRoom)
                {
                    if ( (e.DateStart > startDate && e.DateStart < endDate) || 
                         (e.DateEnd > startDate && e.DateEnd < endDate))
                    {
                        errors.Errors.Add("Specified room is already booked for the specified time");
                        break;
                    }
                }
            }

            if (errors.Errors.Count() > 0)
                return Json(new { status = "fail", errors.Errors}) ;

            Event dbModel = new Event();

            dbModel.AllowSubscription = eventModel.AllowSubscription;
            dbModel.ApplicationUserID = (eventModel.ShowAuthor) ? User.Identity.GetUserId() : null;
            dbModel.OrganizerName = (eventModel.ShowAuthor)? null : eventModel.OrganizerName;
            dbModel.ClassroomId = Int32.Parse(eventModel.RoomId);
            dbModel.Description = eventModel.Description;
            dbModel.Title = eventModel.Title;
            dbModel.DateStart = eventModel.Start;
            dbModel.DateEnd = eventModel.End;
            dbModel.IsPublic = eventModel.IsPublic;

            db.Event.Add(dbModel);
            db.SaveChanges();

            return Json(new { status = "success"});
        }

        [HttpPost]
        public JsonResult CancelEvent(int Id)
        {
            var eventToDelete = db.Event.Find(Id);
            

            if (eventToDelete == null)
            {
                return Json(new { result = "No such event exists in the database"}, JsonRequestBehavior.DenyGet);
            }
            else
            {
                bool isAuthorized = false;
                bool isAdmin = User.IsInRole("admin");
                string userId = User.Identity.GetUserId();

                if (isAdmin || eventToDelete.ApplicationUserID == userId)
                    isAuthorized = true;

                if (!isAuthorized)
                    return Json(new { result = "Request for deletion is not authorized" }, JsonRequestBehavior.DenyGet);

                // isAuthorized 
                db.Event.Remove(eventToDelete);
                db.SaveChanges();
                return Json(new { result = "Success" }, JsonRequestBehavior.DenyGet);
            }
        }

        // Get data necessary to render TimeTable
        [HttpGet]
        public JsonResult GetEventDataForDay(DateTime daySelected)
        {
            var db = new ApplicationDbContext();
            var eventsData = db.Event.Where(e => e.DateStart.Day == daySelected.Day)
                .Select(e => new { e.Id, e.DateStart, e.DateEnd, e.ClassroomId, classRoomTitle = e.ClassRoom.Title, e.Title, e.IsPublic});
            var roomData = eventsData.Select(e => new { e.ClassroomId, e.classRoomTitle } ).Distinct();
            return Json(new { roomData, eventsData } , JsonRequestBehavior.AllowGet);
        }

        // Get euipment data displayed on the panel
        [HttpGet]
        public JsonResult GetEquipmentDataForRoom(int roomId)
        {
            ManagementServices.Implementations.EquipmentManagement equipmentManager = new ManagementServices.Implementations.EquipmentManagement();
            return Json(equipmentManager.GetEquipmentByRoomId(roomId), JsonRequestBehavior.AllowGet);
        }

    }
}