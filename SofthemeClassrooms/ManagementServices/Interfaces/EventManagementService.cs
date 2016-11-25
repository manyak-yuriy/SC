using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace ManagementServices
{
    public interface EventManagement
    {
        IEnumerable<Events> GetAll();
        Events GetById(int id);

        void SubscribeFor(string email, Events e);

        void UnSubscribeFrom(string email, Events e);

        void Add(Events e);

        void Edit(Events e);

        IEnumerable<Events> GetByDate(DateTime date);
    }
}
