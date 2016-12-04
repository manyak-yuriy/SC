namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForeignVisitor")]
    public partial class ForeignVisitor
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public long EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
