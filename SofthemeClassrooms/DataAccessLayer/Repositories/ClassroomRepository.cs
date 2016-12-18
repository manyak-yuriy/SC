using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClassroomRepository : IRepository<ClassRoom, int>
    {
        private ApplicationDbContext dbContext;

        public ClassroomRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public void Delete(IEnumerable<ClassRoom> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ClassRoom room = dbContext.ClassRoom.Find(id);
            if (room != null)
            {
                dbContext.ClassRoom.Remove(room);
                dbContext.SaveChanges();
            }
        }

        public ClassRoom Get(int Id)
        {
            return dbContext.ClassRoom.Find(Id);
        }

        public IEnumerable<ClassRoom> GetAll()
        {
            return dbContext.ClassRoom;
        }

        public void Insert(ClassRoom item)
        {
            throw new NotImplementedException();
        }

        public void Update(ClassRoom item)
        {
            var r = dbContext.ClassRoom.Find(item);
            if (r != null)
            {
                dbContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
