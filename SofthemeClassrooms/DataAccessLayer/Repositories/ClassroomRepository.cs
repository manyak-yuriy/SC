using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    class ClassroomRepository : IRepository<ClassRoom, int>
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
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
