using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccessLayer;

namespace DataAccessLayer
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FinalTest", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<ClassRoom> ClassRoom { get; set; }
        public virtual DbSet<ClassRoomProperty> ClassRoomProperty { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<ForeignVisitor> ForeignVisitor { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

        }
    }
}