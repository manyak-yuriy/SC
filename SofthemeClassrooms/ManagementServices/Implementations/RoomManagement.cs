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

        public IEnumerable<RoomInfo> GetOpenedRooms()
        {
            lock (_locker)
            {
                return db.ClassRooms
                    .GetAll()
                    .Where(r => r.IsBookable)
                    .Select(room =>  new RoomInfo()
                    {
                        Title = room.Title,
                        Capacity = room.Capacity,
                        IsBookable = room.IsBookable,
                        Id = room.Id
                    });
            }
        }

        public IEnumerable<RoomStatus> GetOpenedRoomsStatus(DateTime now)
        {
            lock (_locker)
            {
                var openedRooms = db.ClassRooms.GetAll();

                List<RoomStatus> roomInfos = new List<RoomStatus>();
                foreach (var room in openedRooms.ToList())
                {
                    var roomInfo = new RoomStatus()
                    {
                        Id = room.Id,
                        Title = room.Title
                    };
                    if (!room.IsBookable)
                    {
                        roomInfo.roomStatus = "disabled";
                    }
                    else if (db.Events
                        .GetAll()
                        .Where(e => e.DateStart <= now && e.DateEnd >= now
                        && e.ClassroomId == room.Id)
                        .Count() > 0)
                    {
                        roomInfo.roomStatus = "booked";
                    }
                    else
                    {
                        roomInfo.roomStatus = "available";
                    }
                    roomInfos.Add(roomInfo);
                }

                return roomInfos;
            }
        }
    }
}
