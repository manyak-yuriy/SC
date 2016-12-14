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
                c => c.Title,

                new ClassRoom { Title = "Einstein Classroom", Capacity = 10, IsBookable = true},
                new ClassRoom { Title = "Empty", Capacity = 10, IsBookable = true },
                new ClassRoom { Title = "English", Capacity = 10, IsBookable = true },
                new ClassRoom { Title = "HR office", Capacity = 10, IsBookable = true }, 
                new ClassRoom { Title = "Info центр", Capacity = 10, IsBookable = true},
                new ClassRoom { Title = "Newton Classroom", Capacity = 10, IsBookable = true },
                new ClassRoom { Title = "Tesla Classroom", Capacity = 10, IsBookable = true },
                new ClassRoom { Title = "Web & Marketing 1", Capacity = 10, IsBookable = true },
                new ClassRoom { Title = "Web & Marketing 2", Capacity = 10, IsBookable = true },
                new ClassRoom { Title = "Web & Marketing 3", Capacity = 10, IsBookable = true }
            );

            
        }
    }
}
