using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementServices.Models;
using DataAccessLayer;
using System.Data.Entity.Migrations;

namespace ManagementServices.Implementations
{
    public class EquipmentManager
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

        public Equipment GetEquipmentByTitle(string title)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Equipment.Where(e => e.Title == title).First();
            }
        }
       
        public EquipmentViewModel GetEquipmentByRoomId(int roomId)
        {
            using (var db = new ApplicationDbContext())
            {
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

        public void SetEquipmentByRoomId(int roomId, string roomTitle, EquipmentViewModel equipmentData)
        {
            using (var db = new ApplicationDbContext())
            {
                var room = db.ClassRoom.Find(roomId);

                if (room == null)
                    throw new NullReferenceException("There's no room with specified id");

                room.Capacity = equipmentData.SeatCount;
                room.Title = roomTitle;

                var roomProperties = room.ClassRoomProperty.Where(cp => cp.ClassRoomId == roomId).ToList();

                // boards

                var boards = db.ClassRoomProperty.Where(crp => crp.Equipment.Title == "Board" && crp.ClassRoomId == room.Id).FirstOrDefault();
                var boardData = GetEquipmentByTitle("Board");

                if (boards == null)
                {
                    db.ClassRoomProperty.Add(new ClassRoomProperty { ClassRoomId = room.Id, Equipment = boardData, Quantity = equipmentData.BoardCount });
                }
                else
                {
                    boards.Quantity = equipmentData.BoardCount;
                }


                // laptops

                var laptops = db.ClassRoomProperty.Where(crp => crp.Equipment.Title == "Laptop" && crp.ClassRoomId == room.Id).FirstOrDefault();
                var laptopData = GetEquipmentByTitle("Laptop");

                if (laptops == null)
                {
                    db.ClassRoomProperty.Add(new ClassRoomProperty { ClassRoomId = room.Id, Equipment = laptopData, Quantity = equipmentData.LaptopCount });
                }
                else
                {
                    laptops.Quantity = equipmentData.LaptopCount;
                }

                // printers

                var printers = db.ClassRoomProperty.Where(crp => crp.Equipment.Title == "Printer" && crp.ClassRoomId == room.Id).FirstOrDefault();
                var printerData = GetEquipmentByTitle("Printer");

                if (printers == null)
                {
                    db.ClassRoomProperty.Add(new ClassRoomProperty { ClassRoomId = room.Id, Equipment = printerData, Quantity = equipmentData.PrinterCount });
                }
                else
                {
                    printers.Quantity = equipmentData.PrinterCount;
                }

                // projectors

                var projectors = db.ClassRoomProperty.Where(crp => crp.Equipment.Title == "Projector" && crp.ClassRoomId == room.Id).FirstOrDefault();
                var projectorData = GetEquipmentByTitle("Projector");

                if (projectors == null)
                {
                    db.ClassRoomProperty.Add(new ClassRoomProperty { ClassRoomId = room.Id, Equipment = projectorData, Quantity = equipmentData.ProjectorCount });
                }
                else
                {
                    projectors.Quantity = equipmentData.ProjectorCount;
                }

                db.SaveChanges();
            }
        }

    }
}
