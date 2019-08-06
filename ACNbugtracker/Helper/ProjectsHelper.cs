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
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();

        public List<string> UserInRoleOnProject(int projectId, string roleName)
        {
            var people = new List<string>();

            foreach(var user in UsersOnProject(projectId).ToList())
            {
                if(rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    people.Add(user.Id);
                }
            }
            return people;
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            //go into the database find project table and using the a particular User's Id get the projects they are on and return a true or false value
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return(flag);
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
            if(IsUserOnProject(userId, projectId))
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
            return db.Users.Where(u=>u.Projects.All(p=>p.Id!=projectId)).ToList();
        }

    }
}