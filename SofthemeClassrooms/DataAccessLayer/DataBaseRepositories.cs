using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer
{
    public class DataBaseRepositories : IDatabaseRepositories
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        private ClassroomRepository _classroooms;
        private ClassRoomPropertyRepository _classroomProperties;
        private EquipmentRepository _equipment;
        private EventRepository _events;
        private FeedbackRepositroy _feedback;
        private ForeignVisitorRepository _foreignVisitors;
        private UsersRepository _users;
        private readonly object _locker = new object();

        public IRepository<ClassRoomProperty, int> ClassRoomProperties
        {
            get
            {
                if (_classroomProperties != null)
                {
                    return _classroomProperties;
                }

                lock (_locker)
                {
                    if (_classroomProperties == null)
                    {
                        _classroomProperties = new ClassRoomPropertyRepository(_context);
                    }
                }
                return _classroomProperties;
            }
        }

        public IRepository<ClassRoom, int> ClassRooms
        {
            get
            {
                if (_classroooms != null)
                {
                    return _classroooms;
                }

                lock (_locker)
                {
                    if (_classroooms == null)
                    {
                        _classroooms = new ClassroomRepository(_context);
                    }
                }
                return _classroooms;
            }
        }

        public IRepository<Equipment, int> Equipment
        {
            get
            {
                if (_equipment != null)
                {
                    return _equipment;
                }

                lock (_locker)
                {
                    if (_equipment == null)
                    {
                        _equipment = new EquipmentRepository(_context);
                    }
                }
                return _equipment;
            }
        }

        public IRepository<Event, int> Events
        {
            get
            {
                if (_events != null)
                {
                    return _events;
                }

                lock (_locker)
                {
                    if (_events == null)
                    {
                        _events = new EventRepository(_context);
                    }
                }
                return _events;
            }
        }

        public IRepository<Feedback, int> Feedback
        {
            get
            {
                if (_feedback != null)
                {
                    return _feedback;
                }

                lock (_locker)
                {
                    if (_feedback == null)
                    {
                        _feedback = new FeedbackRepositroy(_context);
                    }
                }
                return _feedback;
            }
        }

        public IRepository<ForeignVisitor, int> ForeignVisitors
        {
            get
            {
                if (_foreignVisitors != null)
                {
                    return _foreignVisitors;
                }

                lock (_locker)
                {
                    if (_foreignVisitors == null)
                    {
                        _foreignVisitors = new ForeignVisitorRepository(_context);
                    }
                }

                return _foreignVisitors;
            }
        }

        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _context.Roles;
            }
        }


        public IRepository<ApplicationUser, string> Users
        {
            get
            {
                if (_users != null)
                {
                    return _users;
                }

                lock (_locker)
                {
                    if (_users == null)
                    {
                        _users = new UsersRepository(_context);
                    }
                }
                return _users;
            }
        }
    }
}
