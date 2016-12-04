namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SofthemeDbContext : DbContext
    {
        public SofthemeDbContext()
            : base("name=SofthemeDbContext")
        {
        }

        public virtual DbSet<ClassRoom> ClassRoom { get; set; }
        public virtual DbSet<ClassRoomProperty> ClassRoomProperty { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<ForeignVisitor> ForeignVisitor { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassRoom>()
                .HasMany(e => e.ClassRoomProperty)
                .WithRequired(e => e.ClassRoom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassRoom>()
                .HasMany(e => e.Event)
                .WithRequired(e => e.ClassRoom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.ClassRoomProperty)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.ForeignVisitor)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Event)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.OrganizerId)
                .WillCascadeOnDelete(false);
        }
    }
}
