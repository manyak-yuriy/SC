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

    public class EventRepository : IRepository<Event, long>
    {
        private ApplicationDbContext dbContext;

        public EventRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public void Delete(IEnumerable<Event> items)
        {
            dbContext.Event.RemoveRange(items);
            dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            Event _event = dbContext.Event.Find(id);
            if (_event != null)
            {
                dbContext.Event.Remove(_event);
                dbContext.SaveChanges();
            }
        }

        public Event Get(long Id)
        {
            return dbContext.Event.Find(Id);
        }

        public IEnumerable<Event> GetAll()
        {
            return dbContext.Event;
        }

        public void Insert(Event item)
        {
            dbContext.Event.Add(item);
            dbContext.SaveChanges();
        }

        public void Update(Event item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
