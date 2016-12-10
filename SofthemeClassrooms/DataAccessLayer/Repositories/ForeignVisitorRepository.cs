using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    class ForeignVisitorRepository : IRepository<ForeignVisitor, int>
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public void Delete(int id)
        {
            var visitor = dbContext.ForeignVisitor.Find(id);
            if(visitor != null)
            {
                dbContext.ForeignVisitor.Remove(visitor);
                dbContext.SaveChanges();
            }
        }

        public ForeignVisitor Get(int Id)
        {
            return dbContext.ForeignVisitor.Find(Id);
        }

        public IEnumerable<ForeignVisitor> GetAll()
        {
            return dbContext.ForeignVisitor;
        }

        public void Update(ForeignVisitor item)
        {
            var visitor = dbContext.ForeignVisitor.Find(item);
            if(visitor != null)
            {
                dbContext.Entry(visitor).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}
