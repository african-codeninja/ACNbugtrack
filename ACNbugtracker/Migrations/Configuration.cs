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

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if(!context.Roles.Any(r=> r.Name == "Admin"))
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



            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            #region priority seeding

            context.TicketPriorities.AddOrUpdate(
                t=> t.Name,
                new TicketPriority { Name = "Immediate", Description = "Highest Priority Level"},
                new TicketPriority { Name = "High", Description = "High Priority Level" },
                new TicketPriority { Name = "Medium", Description = "Medium Priority Level" },
                new TicketPriority { Name = "Low", Description = "low Priority Level" },
                new TicketPriority { Name = "None", Description = "No Priority Level" }
                );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                new TicketStatus { Name = "New / UnAssigned", Description = "" },
                new TicketStatus { Name = "UnAssigned", Description = "" },
                new TicketStatus { Name = "New/ Assigned", Description = "" },
                new TicketStatus { Name = "Assigned", Description = "" },
                new TicketStatus { Name = "In Progress", Description = "" }
                );

            context.TicketTypes.AddOrUpdate(
                t=> t.Name,
                    new TicketType { Name = "Bug / Defect", Description = "Typos,Error,Uresolved code"},
                    new TicketType { Name = "New Functionality request", Description = "Client has added a new functionality not originaly mentioned in requirement/deliverables" },
                    new TicketType { Name = "New Document Request", Description = "" },
                    new TicketType { Name = "Training Request", Description = "Client still has not fully understood a function and need more training" },
                    new TicketType { Name = "Complaint", Description = "General issue customer has found that needs resolution" }
                );
            #endregion

        }   
    }
}
