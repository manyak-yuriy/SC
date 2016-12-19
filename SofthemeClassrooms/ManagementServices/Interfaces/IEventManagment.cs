using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementServices.Interfaces
{
    public interface IEventManagment
    {
        void DeleteEventsOfRoom(int roomId);
        IEnumerable<long> GetIdOfRoomEvents(int roomId);
    }
}
