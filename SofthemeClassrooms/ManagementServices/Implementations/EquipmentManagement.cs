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

        private int GetEquipmentQuantityByTitle(IEnumerable<ClassRoomProperty> roomProperties, string equipmentTitle)
        {
            // Equipment count

            if (roomProperties == null)
                return 0;

            var equipment = roomProperties.Where(rp => (rp.Equipment != null && rp.Equipment.Title == equipmentTitle)).FirstOrDefault();

            if (equipment != null)
                return equipment.Quantity;

            return 0;
        }
       
        public EquipmentViewModel GetEquipmentByRoomId(int roomId)
        {
            var db = new ApplicationDbContext();
            var room = db.ClassRoom.Find(roomId);

            if (room == null)
                throw new NullReferenceException("There's no room with specified id");

            var roomProperties = room.ClassRoomProperty.Where(cp => cp.ClassRoomId == roomId);

            var equipmentData = new EquipmentViewModel
            {
                SeatCount = room.Capacity,
                BoardCount = GetEquipmentQuantityByTitle(roomProperties, "Board"),
                LaptopCount = GetEquipmentQuantityByTitle(roomProperties, "Laptop"),
                PrinterCount = GetEquipmentQuantityByTitle(roomProperties, "Printer"),
                ProjectorCount = GetEquipmentQuantityByTitle(roomProperties, "Projector")
            };

            return equipmentData;
        }
    }
}
