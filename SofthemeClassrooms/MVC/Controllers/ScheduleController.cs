using DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Threading.Tasks;
using ManagementServices.Interfaces;
using ManagementServices.Models;
using Microsoft.AspNet.Identity.Owin;
using WebApplication1.Models.Schedule;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IBusinessLogicFactory _businessLogicFactory;
        public ScheduleController(IBusinessLogicFactory factory)
        {
            _businessLogicFactory = factory;
        }

        [HttpGet]
        public ActionResult ShowSchedule()
        {
            return View();
        }

        // Returns partial view with popup contents for displaying info for a particular event
        [HttpGet]
        public ActionResult GetDisplayEventPartial(int Id)
        {
            var eventInfo = _businessLogicFactory.EventManager.GetEventInfo(Id);

            if (eventInfo == null)
            {
                throw new NullReferenceException("There's no event with specified id!");
            }

            var model = DisplayEventPartialViewModel.ConvertFromEventInfo(eventInfo);

            bool isAuthorized = false;
            bool isAdmin = User.IsInRole("admin");
            string userId = User.Identity.GetUserId();

            if (isAdmin || model.ApplicationUserId == userId)
            {
                isAuthorized = true;
            }


            if (!model.IsPublic && !isAuthorized)
            {
                throw new AccessViolationException("Not enough rigths to view the private event");
            }


            model.CanEdit = isAuthorized;

            model.VisitorCount = _businessLogicFactory.VisitorsManager.CountVisitorsOfEvent(eventInfo.Id);

            return PartialView("~/Views/Schedule/Overlays/DisplayEventPartialView.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult GetEventCancellationPartialView(int Id)
        {
            var e = _businessLogicFactory.EventManager.GetEventInfo(Id);
            return PartialView("~/Views/Schedule/Overlays/CancelEventPartialView.cshtml", e.Title);
        }

        [HttpGet]
        public PartialViewResult GetEventAddPartialView()
        {
            EditEventPartialViewModel viewModel;

            viewModel = new EditEventPartialViewModel();
            viewModel.Start = DateTime.Now;
            viewModel.End = DateTime.Now.AddHours(2);
            viewModel.AllowSubscription = false;
            viewModel.IsPublic = false;
            viewModel.ShowAuthor = true;

            List<SelectListItem> items = new List<SelectListItem>();

            IEnumerable<RoomInfo> availRooms = _businessLogicFactory.RoomManager.GetOpenedRooms();

            foreach (var room in availRooms)
            {
                items.Add(new SelectListItem
                {
                    Text = room.Title,
                    Value = room.Id.ToString()
                }
                );
            }

            ViewBag.RoomIdOptions = items;
            ViewBag.CallBackAction = "AddNewEvent";
            ViewBag.IsNew = true;

            return PartialView("~/Views/Schedule/Overlays/EditEventPartialView.cshtml", viewModel);
        }


        [HttpGet]
        public PartialViewResult GetEventEditPartialView(int eventId)
        {
            var eventEntity = _businessLogicFactory.EventManager.GetEventInfo(eventId);

            if (eventEntity == null)
            {
                throw new NullReferenceException("Event with specified id does not exist");
            }

            EditEventPartialViewModel viewModel = new EditEventPartialViewModel()
            {
                AllowSubscription = (bool)eventEntity.AllowSubscription,
                Description = eventEntity.Description,
                End = eventEntity.DateEnd,
                Start = eventEntity.DateStart,
                Title = eventEntity.Title,
                IsPublic = eventEntity.IsPublic,
                OrganizerName = eventEntity.OrganizerName,
                RoomId = eventEntity.RoomId.ToString()
            };


            viewModel.ShowAuthor = true;


            List<SelectListItem> items = new List<SelectListItem>();

            IEnumerable<RoomInfo> availRooms = _businessLogicFactory.RoomManager.GetOpenedRooms();

            int roomId = eventEntity.RoomId;

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


        [HttpPost]
        public ActionResult AddSubscriber(int eventId, NewSubscriberViewModel subModel)
        {
            var errors = new ErrorModel();


            bool subscriptionResult = _businessLogicFactory
                .VisitorsManager.SubscribeForEvent(subModel.Email, eventId);
            if (!subscriptionResult)
            {
                errors.Errors.Add("Это событие не поддерживает подписку");
            }

            if (!ModelState.IsValid)
            {
                var errorList = from item in ModelState.Values
                                from error in item.Errors
                                select error.ErrorMessage;

                foreach (string s in errorList)
                {
                    errors.Errors.Add(s);
                }
            }

            if (errors.Errors.Count() > 0)
            {
                return Json(new { status = "fail", errors.Errors });
            }


            return Json(new { status = "success" });
        }

        [HttpPost]
        public ActionResult AddNewEvent(EditEventPartialViewModel eventModel)
        {
            var classRoom = _businessLogicFactory
                .RoomManager
                .GetRoomInfo(Int32.Parse(eventModel.RoomId));

            var errors = new ErrorModel();

            if (!ModelState.IsValid)
            {
                var errorList = from item in ModelState.Values
                                from error in item.Errors
                                select error.ErrorMessage;

                foreach (var s in errorList)
                {
                    errors.Errors.Add(s);
                }
            }

            if (eventModel.End - eventModel.Start < new TimeSpan(0, 20, 0))
            {
                errors.Errors.Add("Событие не может быть короче 20 минут");
            }

            if (eventModel.End.Hour > 20 || (eventModel.End.Hour == 20 && eventModel.End.Minute != 0) || eventModel.Start.Hour < 9)
                errors.Errors.Add("Событие не может быть за пределами рабочих часов");

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
                if (!_businessLogicFactory.EventManager
                    .IsRoomForEventFree(int.Parse(eventModel.RoomId),
                                        eventModel.Start, eventModel.End))
                {
                    errors.Errors.Add("Эта комната уже занята в указаное время.");
                }
            }

            if (errors.Errors.Count() > 0)

            {
                return Json(new { status = "fail", errors.Errors });
            }


            EventInfo eventInfo = new EventInfo()
            {
                AllowSubscription = eventModel.AllowSubscription,
                ApplicationUserID = (eventModel.ShowAuthor) ? User.Identity.GetUserId() : null,
                OrganizerName = (eventModel.ShowAuthor) ? null : eventModel.OrganizerName,
                RoomId = Int32.Parse(eventModel.RoomId),
                Description = eventModel.Description,
                Title = eventModel.Title,
                DateStart = eventModel.Start,
                DateEnd = eventModel.End,
                IsPublic = eventModel.IsPublic
            };

            _businessLogicFactory.EventManager.CreateEvent(eventInfo);

            return Json(new { status = "success" });
        }

        public ActionResult EditEvent(int eventId, EditEventPartialViewModel eventModel)
        {
            var classRoom = _businessLogicFactory
                        .RoomManager.GetRoomInfo(int.Parse(eventModel.RoomId));

            var errors = new ErrorModel();

            if (!ModelState.IsValid)
            {
                var errorList = from item in ModelState.Values
                                from error in item.Errors
                                select error.ErrorMessage;

                foreach (string s in errorList)
                {
                    errors.Errors.Add(s);
                }
            }

            if (eventModel.End - eventModel.Start < new TimeSpan(0, 20, 0))
            {
                errors.Errors.Add("Событие не может быть короче 20 минут");
            }

            if (eventModel.End.Hour > 21 || (eventModel.End.Hour == 21 && eventModel.End.Minute != 0) || eventModel.Start.Hour < 9)
                errors.Errors.Add("Событие не может быть за пределами рабочих часов");

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
                var eventsForClassRoom = db.Event.Where(e => e.ClassroomId == classRoom.Id && e.Id != eventId).ToList();

                DateTime startDate = eventModel.Start;
                DateTime endDate = eventModel.End;

                foreach (var e in eventsForClassRoom)
                {
                    if ((e.DateStart > startDate && e.DateStart < endDate) ||
                         (e.DateEnd > startDate && e.DateEnd < endDate))
                    {
                        errors.Errors.Add("Specified room is already booked for the specified time");
                        break;
                    }
                }
            }

            if (errors.Errors.Count() > 0)
            {
                return Json(new { status = "fail", errors.Errors });
            }

            EventInfo eventInfo = new EventInfo()
            {
                Id = eventId,
                AllowSubscription = eventModel.AllowSubscription,
                ApplicationUserID = (eventModel.ShowAuthor) ? User.Identity.GetUserId() : null,
                OrganizerName = (eventModel.ShowAuthor) ? null : eventModel.OrganizerName,
                RoomId = Int32.Parse(eventModel.RoomId),
                Description = eventModel.Description,
                Title = eventModel.Title,
                DateStart = eventModel.Start,
                DateEnd = eventModel.End,
                IsPublic = eventModel.IsPublic
            };

            _businessLogicFactory.EventManager.UpdateEvent(eventInfo);

            return Json(new { status = "success" });
        }

        [HttpPost]
        public async Task<JsonResult> CancelEvent(int Id)
        {
            var eventToDelete = _businessLogicFactory.EventManager.GetEventInfo(Id);


            if (eventToDelete == null)
            {
                return Json(new { result = "No such event exists in the database" }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                bool isAuthorized = false;
                bool isAdmin = User.IsInRole("admin");
                string userId = User.Identity.GetUserId();

                if (isAdmin || eventToDelete.ApplicationUserID == userId)
                {
                    isAuthorized = true;
                }

                if (!isAuthorized)
                {
                    return Json(new { result = "Request for deletion is not authorized" }, JsonRequestBehavior.DenyGet);
                }


                var subscribers = db.ForeignVisitor.Where(fv => fv.EventId == Id).ToList();

                foreach (var subscriber in subscribers)
                    db.ForeignVisitor.Remove(subscriber);

                var foreignVManager = _businessLogicFactory.VisitorsManager;
                var emails = foreignVManager.EmailsOfEventVisitors(Id);
                foreignVManager.DeleteVisiotorsOfCanceledEvent(Id);

                EmailService emailService = new EmailService();
                foreach (var email in emails)
                {
                    await emailService.SendAsync(new IdentityMessage()
                    {
                        Destination = email,
                        Subject = "Событие отменено.",
                        Body = "Событие " + eventToDelete.Title + " что должно было состояться " +
                               eventToDelete.DateStart.ToString("d") + " в " + eventToDelete.DateStart.ToString("t") +
                               " отменено."
                    });
                }


            _businessLogicFactory.EventManager.DeleteEvent(eventToDelete.Id);

                return Json(new { result = "Success" }, JsonRequestBehavior.DenyGet);
            }
        }

        // Get data necessary to render TimeTable
        [HttpGet]
        public JsonResult GetEventDataForDay(DateTime daySelected)
        {
            var eventsData = _businessLogicFactory.EventManager.GetEventTableItems(daySelected);
            var roomData = eventsData.Select(e => new { e.ClassroomId, e.classRoomTitle }).Distinct();
            return Json(new { roomData, eventsData }, JsonRequestBehavior.AllowGet);
        }

        // Get data necessary to render Calendar on the Room page
        [HttpGet]
        public JsonResult GetEventDataForRoom(int roomId)
        {
            var db = new ApplicationDbContext();
            var eventsData = db.Event.Where(e => e.ClassroomId == roomId)
                .Select(e => new { e.Id, e.IsPublic, Title = (e.IsPublic? e.Title: null), e.DateStart, e.DateEnd });
            
            return Json(new { eventsData}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRoomTableState(DateTime timeNow)
        {
            var roomsAvailability = new List<RoomAvailabilityModel>();

            foreach (var room in _businessLogicFactory.RoomManager.GetOpenedRoomsStatus(timeNow))
            {
                roomsAvailability.Add(new RoomAvailabilityModel { RoomId = room.Id, Status = room.roomStatus , RoomTitle = room.Title });
            }

            return Json(new { roomsAvailability }, JsonRequestBehavior.AllowGet);
        }


        // Get euipment data displayed on the panel
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetEquipmentDataForRoom(int roomId)
        {
            var equipmentData = _businessLogicFactory.EquipmentManagment.GetEquipmentByRoomId(roomId);
            return Json(equipmentData, JsonRequestBehavior.AllowGet);
        }


    }
}