using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.Repositories
{
    class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private ClassroomRepository _classroooms;
        private ClassRoomPropertyRepository _classroomProperties;
        private EquipmentRepository _equipment;
        private EventRepository _events;
        private FeedbackRepositroy _feedback;
        private ForeignVisitorRepository _foreignVisitors;
        private UsersRepository _users;

        public ClassroomRepository ClassRooms
        {
            get
            {
                if(_classroooms == null)
                {
                    _classroooms = new ClassroomRepository(context);
                }
                return _classroooms;
            }
        }

        public ClassRoomPropertyRepository ClassRoomProperties
        {
            get
            {
                if(_classroomProperties == null)
                {
                    _classroomProperties = new ClassRoomPropertyRepository(context);
                }
                return _classroomProperties;
            }
        }

        public EquipmentRepository Equipment
        {
            get
            {
                if(_equipment == null)
                {
                    _equipment = new EquipmentRepository(context);
                }
                return _equipment;
            }
        }

        public EventRepository Events
        {
            get
            {
                if(_events == null)
                {
                    _events = new EventRepository(context);
                }
                return _events;
            }
        }

        public FeedbackRepositroy Feedback
        {
            get
            {
                if(_feedback == null)
                {
                    _feedback = new FeedbackRepositroy(context);
                }
                return _feedback;
            }
        }

        public ForeignVisitorRepository ForeignVisitors
        {
            get
            {
                if(_foreignVisitors == null)
                {
                    _foreignVisitors = new ForeignVisitorRepository(context);
                }

                return _foreignVisitors;
            }
        }

        public UsersRepository Users
        {
            get
            {
                if(_users == null)
                {
                    _users = new UsersRepository(context);
                }

                return _users;
            }
        }

        private bool disposed;
        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
