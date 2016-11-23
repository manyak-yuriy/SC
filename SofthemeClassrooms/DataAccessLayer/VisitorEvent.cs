namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisitorEvent")]
    public partial class VisitorEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public long EventId { get; set; }

        public int VisitorId { get; set; }

        public virtual Events Events { get; set; }

        public virtual User User { get; set; }
    }
}
