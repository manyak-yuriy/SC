using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClassRoomPropertyRepository : IRepository<ClassRoomProperty, int>
    {
        ApplicationDbContext dbContext;

        public ClassRoomPropertyRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public void Delete(IEnumerable<ClassRoomProperty> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            ClassRoomProperty property = dbContext.ClassRoomProperty.Find(id);
            if (property != null)
            {
                dbContext.ClassRoomProperty.Remove(property);
                dbContext.SaveChanges();
            }
        }

        public ClassRoomProperty Get(int Id)
        {
            return dbContext.ClassRoomProperty.Find(Id);
        }

        public IEnumerable<ClassRoomProperty> GetAll()
        {
            return dbContext.ClassRoomProperty;
        }

        public void Insert(ClassRoomProperty item)
        {
            throw new NotImplementedException();
        }

        public void Update(ClassRoomProperty item)
        {
            var p = dbContext.ClassRoomProperty.Find(item);
            if (p != null)
            {
                dbContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
