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
    public class EventVisitorActions : IEventVisitorActions
    {
        IDatabaseRepositories db = DBFactory.GetDbRepositories();
        private static object _locker = new object();
        public void DeleteVisiotorsOfCanceledEvent(long EventId)
        {
            lock (_locker)
            {
                var visitors = db.ForeignVisitors.GetAll().Where(v => v.EventId == EventId);
                db.ForeignVisitors.Delete(visitors);
            }
        }

        public void DeleteVisitorsOfCanceledEvents(IEnumerable<long> eventsId)
        {
            List<ForeignVisitor> visitors = new List<ForeignVisitor>();
            lock (_locker)
            {
                foreach (var eventId in eventsId)
                {
                    var vis = db.ForeignVisitors
                        .GetAll()
                        .Where(v => v.EventId == eventId);
                    visitors.AddRange(vis);
                }

                if (visitors.Count() > 0)
                {
                    db.ForeignVisitors.Delete(visitors);
                }
            }
        }


        public IEnumerable<string> EmailsOfEventVisitors(long EventId)
        {
            lock (_locker)
            {
                return db.ForeignVisitors
                .GetAll()
                .Where(v => v.EventId == EventId)
                .Select(x => x.Email);
            }
        }
    }
}
