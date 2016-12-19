using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using ManagementServices.Interfaces;

namespace ManagementServices.Implementations
{
    public class EventManager : IEventManagment
    {
        private IDatabaseRepositories _dbRepositories = DBFactory.GetDbRepositories();
        private object _locker = new object();

        public void DeleteEventsOfRoom(int roomId)
        {
            lock (_locker)
            {
                var events = _dbRepositories
                .Events
                .GetAll()
                .Where(e => e.ClassroomId == roomId);

                if (events.Count() > 0)
                {
                    _dbRepositories.Events.Delete(events);
                }
            }
        }

        public IEnumerable<long> GetIdOfRoomEvents(int roomId)
        {
            lock (_locker)
            {
                var eventsId = _dbRepositories
                    .Events
                    .GetAll()
                    .Where(e => e.ClassroomId == roomId)
                    .Select(id => id.Id);

                return eventsId;
            }
        }
    }
}
