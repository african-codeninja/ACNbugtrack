using ACNbugtracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Helper
{
    public class ProjectsHelper
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        private UserRolesHelper RolesHelper = new UserRolesHelper();

        public List<string> UserInRoleOnProject(int projectId, string roleName)
        {
            var people = new List<string>();

            foreach (var user in UsersOnProject(projectId).ToList())
            {
                if (RolesHelper.IsUserInRole(user.Id, roleName))
                {
                    people.Add(user.FullName);
                }
            }
            return people;
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            //go into the database find project table and using the a particular User's Id get the projects they are on and return a true or false value
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }

        //Lists the projects a user is working on
        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var projects = user.Projects.ToList();

            return (projects);
        }

        //Add user to a project
        public void AddUserToProject(string userId, int projectId)
        {
            //only add if the user doesn't belong to a project
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);

                //The add user to project operation in Database 
                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        //Remove user from project
        public void RemoveUserFromProject(string userId, int projectId)
        {
            //Do this only if user is on a project
            if (IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);

                //The remove project operation in Database
                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified;//Just saves this obj instance.
                db.SaveChanges();
            }
        }

        //For every project list users are working it
        public ICollection<ApplicationUser> UsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }

        //For every project list users are not working on it
        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
        }

        public ICollection<Ticket> ListProjectTickets(int projectId)
        {
            var projectTickets = db.Tickets.Where(t => t.ProjectId == projectId).ToList();
            return (projectTickets);
        }

        public ICollection<Ticket> ListUserTickets(string userId)
        {
            var userTickets = new List<Ticket>();
            var userRole = RolesHelper.ListUserRoles(userId).FirstOrDefault();
            switch (userRole)
            {
                case "Submitter":
                    userTickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();
                    break;
                case "Developer":
                    userTickets = db.Tickets.Where(t => t.AssignedToUserId == userId).ToList();
                    break;
                case "ProjectManager":
                    var user = db.Users.Find(userId);
                    userTickets = user.Projects.SelectMany(p => p.Tickets).ToList();
                    break;
                case "Admin":
                    userTickets = db.Tickets.ToList();
                    break;
                default:
                    break;
            }
            return (userTickets);
        }
    }
}