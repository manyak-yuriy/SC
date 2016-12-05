namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            ForeignVisitor = new HashSet<ForeignVisitor>();
        }

        public long Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int ClassroomId { get; set; }

        public bool IsPublic { get; set; }

        [StringLength(100)]
        public string OrganizerName { get; set; }

        public bool? AllowSubscription { get; set; }

        public virtual ClassRoom ClassRoom { get; set; }

        public virtual ApplicationUser Organizer { get; set; }

        public string ApplicationUserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForeignVisitor> ForeignVisitor { get; set; }
    }
}
