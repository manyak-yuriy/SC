using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    class FeedbackRepositroy : IRepository<Feedback, int>
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public void Delete(int id)
        {
            var feedback = dbContext.Feedback.Find(id);
            if(feedback != null)
            {
                dbContext.Feedback.Remove(feedback);
            }
        }

        public Feedback Get(int Id)
        {
            return dbContext.Feedback.Find(Id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return dbContext.Feedback;
        }

        public void Update(Feedback item)
        {
            dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
