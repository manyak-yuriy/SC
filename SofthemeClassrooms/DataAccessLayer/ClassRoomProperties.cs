namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClassRoomProperties
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public short ClassRoomId { get; set; }

        public int EquipmentId { get; set; }

        public int Quantity { get; set; }

        public virtual ClassRooms ClassRooms { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
