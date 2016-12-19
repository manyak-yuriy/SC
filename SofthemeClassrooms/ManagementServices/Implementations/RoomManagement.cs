using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using ManagementServices.Interfaces;
using ManagementServices.Models;

namespace ManagementServices.Implementations
{
    public class RoomManagement : IRoomManagment
    {
        private readonly IDatabaseRepositories db = DBFactory.GetDbRepositories();
        private static object _locker = new object();
        public void Close(int id)
        {
            lock (_locker)
            {
                var room = db.ClassRooms.Get(id);
                room.IsBookable = false;
                db.SaveDBChanges();
            }
        }

        public void Open(int id)
        {
            lock (_locker)
            {
                var room = db.ClassRooms.Get(id);
                room.IsBookable = true;
                db.SaveDBChanges();
            }
        }

        public RoomInfo GetRoomInfo(int roomId)
        {
            ClassRoom room;
            lock (_locker)
            {
                room = db.ClassRooms.Get(roomId);
            }

            if (room == null)
            {
                return null;
            }

            RoomInfo info = new RoomInfo()
            {
                Title = room.Title,
                Capacity = room.Capacity,
                Id = room.Id,
                IsBookable = room.IsBookable
            };
            return info;
        }
    }
}
