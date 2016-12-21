using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

using DataAccessLayer.Interfaces;
using ManagementServices.Interfaces;
using ManagementServices.Models;

namespace ManagementServices.Implementations
{
    public class EventManager : IEventManagment
    {
        private IDatabaseRepositories _dbRepositories = DBFactory.GetDbRepositories();
        private object _locker = new object();

        public void CreateEvent(EventInfo eventInfo)
        {
            var e = new Event()
            {
                AllowSubscription = eventInfo.AllowSubscription,
                ApplicationUserID = eventInfo.ApplicationUserID,
                ClassroomId = eventInfo.RoomId,
                DateStart = eventInfo.DateStart,
                DateEnd = eventInfo.DateEnd,
                Description = eventInfo.Description,
                IsPublic = eventInfo.IsPublic,
                Title = eventInfo.Title
            };

            lock (_locker)
            {
                _dbRepositories.Events.Insert(e);
            }
        }

        public void DeleteEvent(long eventId)
        {
            _dbRepositories.Events.Delete(eventId);
        }

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

        public EventInfo GetEventInfo(long eventId)
        {
            lock (_locker)
            {
                var e = _dbRepositories.Events.Get(eventId);

                if (e == null)
                {
                    return null;
                }


                var info = new EventInfo()
                {
                    Id = e.Id,
                    AllowSubscription = e.AllowSubscription ?? false,
                    Title = e.Title,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                    OrganizerName = e.OrganizerName ?? e.Organizer.UserName,
                    Description = e.Description,
                    ApplicationUserID = e.ApplicationUserID,
                    IsPublic = e.IsPublic,
                    RoomId = e.ClassroomId
                };


                return info;
            }
        }

        public IEnumerable<EventInfo> GetEventsForDay(DateTime day)
        {
            lock (_locker)
            {
                return _dbRepositories.Events
                    .GetAll()
                    .Where(e => e.DateStart.Day == day.Day)
                    .Select(e => new EventInfo()
                    {
                        Id = e.Id,
                        AllowSubscription = e.AllowSubscription ?? false,
                        Title = e.Title,
                        DateStart = e.DateStart,
                        DateEnd = e.DateEnd,
                        OrganizerName = e.OrganizerName ?? e.Organizer.UserName,
                        Description = e.Description,
                        ApplicationUserID = e.ApplicationUserID,
                        IsPublic = e.IsPublic,
                        RoomId = e.ClassroomId
                    });
            }
        }

        public IEnumerable<EventTableItem> GetEventTableItems(DateTime day)
        {
            lock (_locker)
            {
                var events =_dbRepositories.Events.GetAll()
                    .Where(e => e.DateStart.Day == day.Day).ToList();

                return events.Select(e => new EventTableItem()
                {
                    ClassroomId = e.ClassroomId,
                    classRoomTitle = e.ClassRoom.Title,
                    DateStart = e.DateStart,
                    DateEnd = e.DateEnd,
                    Id = e.Id,
                    IsPublic = e.IsPublic,
                    Title = e.Title
                });
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

        public bool IsRoomForEventFree(int roomId, DateTime start, DateTime end)
        {
            lock (_locker)
            {

                var events = _dbRepositories
                    .Events
                    .GetAll()
                    .Where(e => e.ClassroomId == roomId &&
                                (e.DateStart >= start && e.DateStart <= end) ||
                                (e.DateEnd >= start && e.DateEnd <= end)
                    );

                return events.Count() == 0;
            }
        }

        public void UpdateEvent(EventInfo eventInfo)
        {
            lock (_locker)
            {
                var e = _dbRepositories.Events.Get(eventInfo.Id);
                e.AllowSubscription = eventInfo.AllowSubscription;
                e.ApplicationUserID = eventInfo.ApplicationUserID;
                e.ClassroomId = eventInfo.RoomId;
                e.DateStart = eventInfo.DateStart;
                e.DateEnd = eventInfo.DateEnd;
                e.IsPublic = eventInfo.IsPublic;
                e.OrganizerName = eventInfo.OrganizerName;
                e.Description = eventInfo.Description;
                e.Title = eventInfo.Title;

                _dbRepositories.SaveDBChanges();
            }
        }
    }
}
