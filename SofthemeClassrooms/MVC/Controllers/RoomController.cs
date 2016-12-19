using DataAccessLayer;
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