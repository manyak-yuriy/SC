using DataAccessLayer;
using ManagementServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementServices.Interfaces;

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

        private IBusinessLogicFactory _businessLogicFactory;
		
        public RoomController(IBusinessLogicFactory factory)
        {
            _businessLogicFactory = factory;
        }

        [HttpPost]
        public ActionResult Close(int roomId)
        {

            var roomManagment = _businessLogicFactory.RoomManager;
            roomManagment.Close(roomId);

            var eventManagment = _businessLogicFactory.EventManager;
            var eventsId = eventManagment.GetIdOfRoomEvents(roomId);
            _businessLogicFactory.VisitorsManager.DeleteVisitorsOfCanceledEvents(eventsId);

            eventManagment.DeleteEventsOfRoom(roomId);
            
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Open(int roomId)
        {
            _businessLogicFactory.RoomManager.Open(roomId);

            return new EmptyResult();
        }
              
        [HttpGet]
        public ActionResult Index(int roomId)
        {
            var roomInfo = _businessLogicFactory.RoomManager.GetRoomInfo(roomId);

            if (roomInfo == null)
            {
                return new HttpNotFoundResult();
            }

            ViewBag.isBookable = roomInfo.IsBookable;
            ViewBag.roomId = roomId;
            return View();

        }
    }
}