using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;

namespace ManagementServices.Interfaces
{
    public interface IRoomManagment
    {
        void Close(int id);
        void Open(int id);
        RoomInfo GetRoomInfo(int roomId);
    }
}
