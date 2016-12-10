using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class UsersRepository : IRepository<ApplicationUser, string>
    {
        ApplicationDbContext dbContext;

        public UsersRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public void Delete(string id)
        {
            var user = dbContext.Users.Find(id);
            if(user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }

        }

        public ApplicationUser Get(string Id)
        {
            return dbContext.Users.Find(Id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return dbContext.Users;
        }

        public void Insert(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser item)
        {
            var user = dbContext.Users.Find(item);
            if(user != null)
            {
                dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
