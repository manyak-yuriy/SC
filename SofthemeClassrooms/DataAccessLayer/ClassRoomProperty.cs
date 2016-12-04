namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassRoomProperty")]
    public partial class ClassRoomProperty
    {
        public int Id { get; set; }

        public int ClassRoomId { get; set; }

        public int EquipmentId { get; set; }

        public int Quantity { get; set; }

        public virtual ClassRoom ClassRoom { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
