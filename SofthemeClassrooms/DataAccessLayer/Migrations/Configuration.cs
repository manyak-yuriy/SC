namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(
                r => r.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "user"},
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "admin" }
            );


            context.ClassRoom.AddOrUpdate(
                c => c.Id,

                new ClassRoom { Id = 1, Title = "Einstein Classroom", Capacity = 20, IsBookable = true},
                new ClassRoom { Id = 2, Title = "Empty", Capacity = 3, IsBookable = false },
                new ClassRoom { Id = 3, Title = "English", Capacity = 13, IsBookable = true },
                new ClassRoom { Id = 4, Title = "HR office", Capacity = 12, IsBookable = true }, 
                new ClassRoom { Id = 5, Title = "Info центр", Capacity = 11, IsBookable = true},
                new ClassRoom { Id = 6, Title = "Newton Classroom", Capacity = 15, IsBookable = true },
                new ClassRoom { Id = 7, Title = "Tesla Classroom", Capacity = 19, IsBookable = true },
                new ClassRoom { Id = 8, Title = "Web & Marketing", Capacity = 4, IsBookable = false },
                new ClassRoom { Id = 9, Title = "Web & Marketing", Capacity = 5, IsBookable = true },
                new ClassRoom { Id = 10, Title = "Web & Marketing", Capacity = 7, IsBookable = false }
            );

            context.Equipment.AddOrUpdate(e => e.Id,
                new Equipment { Id = 1, ImagePath = "Empty", Title = "Board"},
                new Equipment { Id = 2, ImagePath = "Empty", Title = "Laptop" },
                new Equipment { Id = 3, ImagePath = "Empty", Title = "Printer" },
                new Equipment { Id = 4, ImagePath = "Empty", Title = "Projector" });
        }
    }
}
