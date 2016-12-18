using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ManagementServices.Interfaces;

namespace ManagementServices.Implementations
{
    public class EventActions : IEventActions
    {
        public void Add(Event e)
        {
            throw new NotImplementedException();
        }

        public void Edit(Event e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Event GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SubscribeFor(string email, Event e)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeFrom(string email, Event e)
        {
            throw new NotImplementedException();
        }
    }
}
