using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    public interface IEventVisitorActions
    {
        void DeleteVisiotorsOfCanceledEvent(long EventId);
        void DeleteVisitorsOfCanceledEvents(IEnumerable<long> eventsId);
        IEnumerable<string> EmailsOfEventVisitors(long EventVisitors);
    }
}
