using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;
using DataAccessLayer;

namespace ManagementServices.Implementations
{
    public class EquipmentManagement
    {
        public EquipmentViewModel GetEquipmentByRoomId(int roomId)
        {
            Random r = new Random();

            return new EquipmentViewModel
            {
                SeatCount = 10 + r.Next(0, 3),
                BoardCount = 1 + r.Next(0, 2),
                LaptopCount = 10 + r.Next(0, 10),
                PrinterCount = 0 + r.Next(0, 3),
                ProjectorCount = 0 + r.Next(0, 2)
            };

            
        }
    }
}
