namespace ACNbugtracker.Migrations
{
    using ACNbugtracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ACNbugtracker.Models.ApplicationDbContext>
    {


        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ACNbugtracker.Models.ApplicationDbContext context)
        {
            #region roleManager

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion
            //  This method will be called after migrating to the latest version.            
            //I need to create several users that will eventually occupy the roles of either Admin, Project Manager, Developer and Submitter
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "mosesmwangi@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Moses",
                    LastName = "Mwangi",
                    DisplayName = "Boss",
                    AvatarUrl = "/Uploads/bald-head-147977_640.png",
                    Email = "mosesmwangi@mailinator.com",
                    UserName = "mosesmwangi@mailinator.com"

                }, "Abc&123");

                if (!context.Users.Any(u => u.Email == "zuri@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Zuri",
                        LastName = "Mbutha",
                        DisplayName = "Princess",
                        AvatarUrl = "/Uploads/avatar-160055_640.png",
                        Email = "zuri@mailinator.com",
                        UserName = "zuri@mailinator.com"

                    }, "Zuri@1234");
                }

                if (!context.Users.Any(u => u.Email == "josephine@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Josephine",
                        LastName = "Ngina",
                        DisplayName = "Queen",
                        AvatarUrl = "/Uploads/secondlife-1625903_640.jpg",
                        Email = "josephine@mailinator.com",
                        UserName = "josephine@mailinator.com"
                    }, "Ngina@1234");
                }

                if (!context.Users.Any(u => u.Email == "bryant@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Bryant",
                        LastName = "Caldwell",
                        DisplayName = "2hoursaway",
                        AvatarUrl = "/Uploads/man-3414477_640.png",
                        Email = "bryant@mailinator.com",
                        UserName = "bryant@mailinator.com"

                    }, "Bryant@1234");
                }

                if (!context.Users.Any(u => u.Email == "dimitri@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Dimitri",
                        LastName = "Carter",
                        DisplayName = "Curses",
                        AvatarUrl = "/Uploads/goaty-149860_640.png",
                        Email = "dimitri@mailinator.com",
                        UserName = "dimitri@mailinator.com"

                    }, "dimitri@1234");
                }

                if (!context.Users.Any(u => u.Email == "srijana@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Srijana",
                        LastName = "Karki",
                        DisplayName = "Coder",
                        AvatarUrl = "/Uploads/avatar-2027367_640.png",
                        Email = "srijana@mailinator.com",
                        UserName = "srijana@mailinator.com"

                    }, "srijana@1234");
                }

                if (!context.Users.Any(u => u.Email == "hunter@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Hunter",
                        LastName = "Williams",
                        DisplayName = "Hunter",
                        AvatarUrl = "/Uploads/cartoon-1890438_640.jpg",
                        Email = "hunter@mailinator.com",
                        UserName = "hunter@mailinator.com"

                    }, "hunter@1234");
                }

                if (!context.Users.Any(u => u.Email == "lisa@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Lisa",
                        LastName = "Kaifer",
                        DisplayName = "LisaDev",
                        AvatarUrl = "/Uploads/avatar-160055_640.png",
                        Email = "lisa@mailinator.com",
                        UserName = "lisa@mailinator.com"

                    }, "lisa@1234");
                }

                if (!context.Users.Any(u => u.Email == "diego@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Diego",
                        LastName = "Rangel",
                        DisplayName = "Exploer",
                        AvatarUrl = "/Uploads/avatar-1606916_640.png",
                        Email = "diego@mailinator.com",
                        UserName = "diego@mailinator.com"

                    }, "diego@1234");
                }

                if (!context.Users.Any(u => u.Email == "jalpa@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Jalpa",
                        LastName = "Patel",
                        DisplayName = "C#Guru",
                        AvatarUrl = "/Uploads/avatar-2027369_640.png",
                        Email = "jalpa@mailinator.com",
                        UserName = "jalpa@mailinator.com"

                    }, "jalpa@1234");
                }

                if (!context.Users.Any(u => u.Email == "joeshmoe@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Joe",
                        LastName = "Shmoe",
                        DisplayName = "Other",
                        AvatarUrl = "/Uploads/bald-head-147977_640.png",
                        Email = "Joeshmoe@mailinator.com",
                        UserName = "joeshmoe@mailinator.com"

                    }, "joe@1234");
                }

                if (!context.Users.Any(u => u.Email == "adam@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Adam",
                        LastName = "Brooks",
                        DisplayName = "joker",
                        AvatarUrl = "/Uploads/man-3414477_640.png",
                        Email = "adam@mailinator.com",
                        UserName = "adam@mailinator.com"

                    }, "adam@1234");
                }

                if (!context.Users.Any(u => u.Email == "derrick@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Derrick",
                        LastName = "Gordon",
                        DisplayName = "Flash",
                        AvatarUrl = "/Uploads/man-3357275_640.png",
                        Email = "derrick@mailinator.com",
                        UserName = "derrick@mailinator.com"

                    }, "derrick&1234");
                }

                if (!context.Users.Any(u => u.Email == "david@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "David",
                        LastName = "Etzel",
                        DisplayName = "Ergo",
                        AvatarUrl = "/Uploads/man-1721463_640.png",
                        Email = "david@mailinator.com",
                        UserName = "david@mailinator.com"

                    }, "david&1234");
                }

                if (!context.Users.Any(u => u.Email == "kerry@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "Kerry",
                        LastName = "Peary",
                        DisplayName = "silent",
                        AvatarUrl = "/Uploads/bald-head-147977_640.png",
                        Email = "kerry@mailinator.com",
                        UserName = "kerry@mailinator.com"

                    }, "kerry&1234");
                }

                if (!context.Users.Any(u => u.Email == "willy@mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        FirstName = "William",
                        LastName = "Laspata",
                        DisplayName = "Will",
                        AvatarUrl = "/Uploads/avatar-3637425_640.png",
                        Email = "willy@mailinator.com",
                        UserName = "willy@mailinator.com"

                    }, "william&1234");
                }
            }

            var userId = userManager.FindByEmail("mosesmwangi@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("joeshmoe@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            #region priority seeding

            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                new TicketPriority { Name = "Immediate", Description = "Highest Priority Level" },
                new TicketPriority { Name = "High", Description = "High Priority Level" },
                new TicketPriority { Name = "Medium", Description = "Medium Priority Level" },
                new TicketPriority { Name = "Low", Description = "low Priority Level" },
                new TicketPriority { Name = "None", Description = "No Priority Level" }
                );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                new TicketStatus { Name = "New/UnAssigned", Description = "" },
                new TicketStatus { Name = "UnAssigned", Description = "" },
                new TicketStatus { Name = "New/Assigned", Description = "" },
                new TicketStatus { Name = "Assigned", Description = "" },
                new TicketStatus { Name = "In Progress", Description = "" }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Bug / Defect", Description = "Typos,Error,Uresolved code" },
                    new TicketType { Name = "New Functionality request", Description = "Client has added a new functionality not originaly mentioned in requirement/deliverables" },
                    new TicketType { Name = "New Document Request", Description = "" },
                    new TicketType { Name = "Training Request", Description = "Client still has not fully understood a function and need more training" },
                    new TicketType { Name = "Complaint", Description = "General issue customer has found that needs resolution" }
                );
            #endregion

        }
    }
}
