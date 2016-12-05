﻿using DataAccessLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            db.Equipment.Add(new Equipment { Title ="TV", ImagePath = "F:"});

            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            var classRoom = new ClassRoom { Title = "Tesla", Capacity = 12, IsBookable = true};

            var ev = new Event { AllowSubscription = true, ApplicationUserID = User.Identity.GetUserId(), ClassRoom = classRoom, DateStart = DateTime.Now, DateEnd = DateTime.MaxValue, Title = "QA intro", Description = "Cool event", IsPublic = true};

            db.Event.Add(ev);

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