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

        public void DeleteVisiotrOfCanceledEvent(long EventId)
        {
            var visitors = db.ForeignVisitors.GetAll().Where(v => v.EventId == EventId);
            db.ForeignVisitors.Delete(visitors);
        }

        public IEnumerable<string> EmailsOfEventVisitors(long EventId)
        {
            return db.ForeignVisitors
                .GetAll()
                .Where(v => v.EventId == EventId)
                .Select(x => x.Email);
        }
    }
}
