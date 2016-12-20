using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    public interface IEventVisitorActions
    {
        void DeleteVisiotrOfCanceledEvent(long EventId);
        IEnumerable<string> EmailsOfEventVisitors(long EventVisitors);
    }
}
