using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;

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
                BoardCount = 2 + r.Next(0, 3),
                LaptopCount = 10 + r.Next(0, 3),
                PrinterCount = 0 + r.Next(0, 3),
                ProjectorCount = 1 + r.Next(0, 3)
            };
        }
    }
}
