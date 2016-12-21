using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using ManagementServices.Models;

namespace ManagementServices.Interfaces
{
    public interface IEquipmentManagment
    {
        EquipmentViewModel GetEquipmentByRoomId(int roomId);
    }
}
