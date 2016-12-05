using DataAccessLayer;
using ManagementServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Implementations
{
    class EventManager : IEventManager
    {
        public void CancelEvent(int id)
        {
            
        }

        public void MakeEvent()
        {
            throw new NotImplementedException();
        }

        public int NumberOfPlanedEvents(string OrgName)
        {
            DbRepository repository = new DbRepository();
            return repository.Events.Where(c => c.OrganizerName == OrgName).Count();
        }

        public void UpdateEvent()
        {
            throw new NotImplementedException();
        }
    }
}
