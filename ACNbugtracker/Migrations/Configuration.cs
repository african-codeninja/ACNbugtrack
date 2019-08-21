namespace ACNbugtracker.Migrations
{
    using ACNbugtracker.Helper;
    using ACNbugtracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

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
            }

                if (!context.Users.Any(u => u.Email == "Admin@Mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "Admin@Mailinator.com",
                        Email = "Admin@Mailinator.com",
                        FirstName = "Admin",
                        LastName = "Administrator",
                        DisplayName = "The Admin"
                    }, "Abc&123!");
                }

                if (!context.Users.Any(u => u.Email == "ProjectManager@Mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "ProjectManager@Mailinator.com",
                        Email = "ProjectManager@Mailinator.com",
                        FirstName = "Project",
                        LastName = "Manager",
                        DisplayName = "The PM"
                    }, "Abc&123!");
                }

                if (!context.Users.Any(u => u.Email == "Developer@Mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "Developer@Mailinator.com",
                        Email = "Developer@Mailinator.com",
                        FirstName = "John",
                        LastName = "Wick",
                        DisplayName = "Ticket Worker"
                    }, "Abc&123!");
                }

                if (!context.Users.Any(u => u.Email == "Submitter@Mailinator.com"))
                {
                    userManager.Create(new ApplicationUser
                    {
                        UserName = "Submitter@Mailinator.com",
                        Email = "Submitter@Mailinator.com",
                        FirstName = "Sub",
                        LastName = "Mitter",
                        DisplayName = "Ticket Creator"
                    }, "Abc&123!");
                }
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
            //Introduce my Demo users
            if (!context.Users.Any(u => u.Email == "DemoAdmin@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "DemoAdmin",
                    LastName = "DemoAdministrator",
                    DisplayName = "The Demo Admin"
                },  WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@Mailinator.com",
                    Email = "DemoProjectManager@Mailinator.com",
                    FirstName = "DemoPM",
                    LastName = "Manager",
                    DisplayName = "The Demo PM"
                },  WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "DemoDev",
                    LastName = "Developer",
                    DisplayName = "Developer"
                },  WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@Mailinator.com",
                    Email = "DemoSubmitter@Mailinator.com",
                    FirstName = "DemoSubmit",
                    LastName = "Submitter",
                    DisplayName = "Submitter"
                },  WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            #region Role Assignment

            var userId = userManager.FindByEmail("mosesmwangi@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            var pmId = userManager.FindByEmail("joeshmoe@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            var adminId = userManager.FindByEmail("Admin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            adminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            pmId = userManager.FindByEmail("DemoProjectManager@Mailinator.com").Id;
            userManager.AddToRole(pmId, "ProjectManager");

            pmId = userManager.FindByEmail("ProjectManager@Mailinator.com").Id;
            userManager.AddToRole(pmId, "ProjectManager");
            
            var subId = userManager.FindByEmail("Submitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            var devId = userManager.FindByEmail("Developer@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

            devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

            #endregion

            #region Project Creation
            context.Projects.AddOrUpdate(
               p => p.Name,
                   new Project { Name = "Mos Blog", Description = "This is the Mos Blog project that is now out in the wild.", Created = DateTime.Now },
                   new Project { Name = "Moses Portfolio", Description = "This is the Portfolio project that is now out in the wild.", Created = DateTime.Now },
                   new Project { Name = "ACN BugTracker", Description = "This is the ACN BugTracker project that is now out in the wild.", Created = DateTime.Now }
               );

            context.SaveChanges();
            #endregion

            #region Project Assignment
            var portfolioProjectId = context.Projects.FirstOrDefault(p => p.Name == "Moses Portfolio").Id;
            var blogProjectId = context.Projects.FirstOrDefault(p => p.Name == "Mos Blog").Id;
            var bugTrackerProjectId = context.Projects.FirstOrDefault(p => p.Name == "ACN BugTracker").Id;
            
            var projectHelper = new ProjectsHelper();

            //Assign all three users to the Potfolio project
            ProjectsHelper.AddUserToProject(pmId, portfolioProjectId);
            ProjectsHelper.AddUserToProject(devId, portfolioProjectId);
            ProjectsHelper.AddUserToProject(subId, portfolioProjectId);
            
            //Assign all three users to the Blog project
            ProjectsHelper.AddUserToProject(pmId, blogProjectId);
            ProjectsHelper.AddUserToProject(devId, blogProjectId);
            ProjectsHelper.AddUserToProject(subId, blogProjectId);

            //Assign all three users to the Blog project
            ProjectsHelper.AddUserToProject(pmId, bugTrackerProjectId);
            ProjectsHelper.AddUserToProject(devId, bugTrackerProjectId);
            ProjectsHelper.AddUserToProject(subId, bugTrackerProjectId);
            #endregion


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
                new TicketStatus { Name = "New / UnAssigned", Description = "New Ticket Not Assigned to any developer yet" },
                new TicketStatus { Name = "UnAssigned", Description = "This is not a new ticket but it still has not been assigned to any developer" },
                new TicketStatus { Name = "New / Assigned", Description = "This ticket has been newly assigned to a developer" },
                new TicketStatus { Name = "Assigned", Description = "This is a new  ticket that has been reassigned to another developer" },
                new TicketStatus { Name = "In Progress", Description = "This is an old ticket still in the process of bieng resolved" },
                new TicketStatus { Name = "Completed", Description = "This is a ticket that has already been resolved" },
                new TicketStatus { Name = "Archived", Description = "Already completed ticket that has been achived" }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                new TicketType { Name = "Bug", Description = "An error has occurred that resulted in either the application crashing or the user seeing error information" },
                new TicketType { Name = "Defect", Description = "An error has occurred that resulted in either a miscalculation or an in correct workflow" },
                new TicketType { Name = "New Functionality request", Description = "Client has added a new functionality not originaly mentioned in requirement/deliverables" },
                new TicketType { Name = "New Document Request", Description = "A client has called in asking for new documentation to be created for the existing application" },
                new TicketType { Name = "Training Request", Description = "Client still has not fully understood a function and need more training" },
                new TicketType { Name = "Complaint", Description = "A client has called in to make a general complaint about our application" },
                new TicketType { Name = "Other", Description = "A call has been received that requires follow up but is outside the usual parameters for a request" }
                );

            context.SaveChanges();
            #endregion

            #region Ticket creation          
            context.Tickets.AddOrUpdate(
               p => p.Title,
                //1 unassigned Bug on the Blog project
                //1 assigned Defect on the Blog project
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //1 unassigned Bug on the Portfolio project
                //1 assigned Defect on the Portfolio project
                new Ticket
                {
                    ProjectId = portfolioProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = portfolioProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //1 unassigned Bug on the BugTracker
                //1 assigned Defect on the BugTracker
                
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                });
            #endregion
        }

    }
    
}
