using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;

namespace ManagementServices.Interfaces
{
    public interface IEventManagment
    {
        void CreateEvent(EventInfo eventInfo);
        void UpdateEvent(EventInfo eventInfo);
        void DeleteEvent(long eventId);
        void DeleteEventsOfRoom(int roomId);
        IEnumerable<long> GetIdOfRoomEvents(int roomId);
        IEnumerable<EventInfo> GetEventsForDay(DateTime day);
        IEnumerable<EventTableItem> GetEventTableItems(DateTime day);
        EventInfo GetEventInfo(long eventId);
        bool IsRoomForEventFree(int roomId, DateTime start, DateTime end);
    }
}
