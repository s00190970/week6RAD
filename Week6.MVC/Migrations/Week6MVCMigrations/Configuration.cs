namespace Week6.MVC.Migrations.Week6MVCMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Week6.Club.DataDomain;
    using Week6.MVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Week6.MVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Week6MVCMigrations";
        }

        protected override void Seed(Week6.MVC.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Admin" }
                );
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "ClubAdmin" }
                );
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "member" }
                );

            context.SaveChanges();

            PasswordHasher ps = new PasswordHasher();
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "admin@itsligo.ie",
                    Email = "admin@itsligo.ie",
                    EntityID = "admin",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = ps.HashPassword("admin")
                });

            ApplicationUser admin = manager.FindByEmail("admin@itsligo.ie");
            manager.AddToRoles(admin.Id, "Admin");

            Week6DbContext dbContext = new Week6DbContext();
            var clubMember = dbContext.Clubs.First().clubMembers.First();
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = clubMember.studentMember.Firstname+"."+clubMember.studentMember.Surname+"@mail.itsligo.ie",
                    Email = clubMember.studentMember.Firstname + "." + clubMember.studentMember.Surname + "@mail.itsligo.ie",
                    EntityID = clubMember.studentMember.StudentNumber,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = ps.HashPassword("clubadmin")
                });
            context.SaveChanges();

            ApplicationUser clubAdmin = manager.FindByEmail(clubMember.studentMember.Firstname + "." + clubMember.studentMember.Surname + "@mail.itsligo.ie");
            manager.AddToRoles(clubAdmin.Id, "ClubAdmin");

            var memberList = (from s in dbContext.Clubs.First().clubMembers
                               select new
                               {
                                  s.StudentNumber,
                                  s.studentMember.Firstname, s.studentMember.Surname
                               }).OrderByDescending(s => s.StudentNumber).Take(9).ToList();

            foreach(var member in memberList)
            {
                context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = member.Firstname + "." + member.Surname + "@mail.itsligo.ie",
                    Email = member.Firstname + "." + member.Surname + "@mail.itsligo.ie",
                    EntityID = member.StudentNumber,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = ps.HashPassword("pass")
                });
            }
        }
    }
}
