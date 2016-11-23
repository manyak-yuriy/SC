namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Events()
        {
            VisitorEvent = new HashSet<VisitorEvent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public short Duration { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public short ClassroomId { get; set; }

        public int OrganizerId { get; set; }

        public bool IsPublic { get; set; }

        public virtual ClassRooms ClassRooms { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitorEvent> VisitorEvent { get; set; }
    }
}
