using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Interfaces
{
    public interface IDatabaseRepositories
    {
        IRepository<ClassRoom, int> ClassRooms { get; }
        IRepository<ClassRoomProperty, int> ClassRoomProperties { get; }
        IRepository<Equipment, int> Equipment { get; }
        IRepository<Event, int> Events { get; }
        IRepository<Feedback, int> Feedback { get; }
        IRepository<ForeignVisitor, int> ForeignVisitors { get; }
        IRepository<ApplicationUser, string> Users { get; }
        IQueryable<IdentityRole> Roles { get; }
        void SaveDBChanges();
    }
}
