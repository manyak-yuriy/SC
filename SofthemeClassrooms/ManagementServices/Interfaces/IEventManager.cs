using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    interface IEventManager
    {
        int NumberOfPlanedEvents(string userId);
        void CancelEvent(int id);
        void MakeEvent();
        void UpdateEvent();
    }
}
