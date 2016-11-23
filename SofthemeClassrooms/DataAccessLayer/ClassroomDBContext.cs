namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClassroomDBContext : DbContext
    {
        public ClassroomDBContext()
            : base("name=ClassroomDBContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<ClassRoomProp> ClassRoomProp { get; set; }
        public virtual DbSet<ClassRooms> ClassRooms { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VisitorEvent> VisitorEvent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<ClassRooms>()
                .HasMany(e => e.ClassRoomProp)
                .WithRequired(e => e.ClassRooms)
                .HasForeignKey(e => e.ClassRoomId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassRooms>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.ClassRooms)
                .HasForeignKey(e => e.ClassroomId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.ClassRoomProp)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Events>()
                .HasMany(e => e.VisitorEvent)
                .WithRequired(e => e.Events)
                .HasForeignKey(e => e.EventId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.OrganizerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.VisitorEvent)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.VisitorId)
                .WillCascadeOnDelete(false);
        }
    }
}
