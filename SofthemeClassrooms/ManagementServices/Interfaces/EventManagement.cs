using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace ManagementServices.Interfaces
{
    public interface IEventManagement
    {
        IEnumerable<Event> GetAll();
        Event GetById(int id);

        void SubscribeFor(string email, Event e);

        void UnSubscribeFrom(string email, Event e);

        void Add(Event e);

        void Edit(Event e);

        IEnumerable<Event> GetByDate(DateTime date);
    }
}
