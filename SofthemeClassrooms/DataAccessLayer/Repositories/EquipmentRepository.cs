using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    class EquipmentRepository : IRepository<Equipment, int>
    {
        ApplicationDbContext dbContext;

        public EquipmentRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Equipment equipment = dbContext.Equipment.Find(id);
            if (equipment != null)
            {
                dbContext.Equipment.Remove(equipment);
                dbContext.SaveChanges();
            }
        }

        public Equipment Get(int Id)
        {
            return dbContext.Equipment.Find(Id);
        }

        public IEnumerable<Equipment> GetAll()
        {
            return dbContext.Equipment;
        }

        public void Update(Equipment item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
