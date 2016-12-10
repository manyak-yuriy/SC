using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Data.Entity.Migrations;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
  
    class EventRepository : IRepository<Event, int>
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public void Delete(int id)
        {
            Event _event = dbContext.Event.Find(id);
            if (_event != null)
            {
                dbContext.Event.Remove(_event);
                dbContext.SaveChanges();
            }
        }

        public Event Get(int Id)
        {
            return dbContext.Event.Find(Id);
        }

        public IEnumerable<Event> GetAll()
        {
            return dbContext.Event;
        }

        public void Update(Event item)
        {
            var e = dbContext.Event.Find(item);
            if (e != null)
            {
                dbContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
