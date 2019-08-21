using ACNbugtracker.Models;
using ACNbugtracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Helper
{
    public class ProjectNotificationHelper : CommonHelper
    {      
        public static void TriggerProjectAssignmentNotifications(List<ApplicationUser> oldProjectUsers, int projectId)
        {
            //want to compare the old project users to the return value of users on project project.Id
            var newUsersOnProject = ProjectsHelper.UsersOnProject(projectId);
            var newUsersNotOnProject = ProjectsHelper.UsersNotOnProject(projectId);
            foreach (var user in newUsersOnProject)
            {
                if (!oldProjectUsers.Select(u => u.Id).Contains(user.Id))
                {
                    //user has been assigned
                    AddProjectAssignmentNotification(projectId, user.Id);
                }
            }
            foreach (var user in newUsersNotOnProject)
            {
                if (oldProjectUsers.Select(u => u.Id).Contains(user.Id))
                {
                    //user has been unassigned
                    AddProjectUnassignmentNotification(projectId, user.Id);
                }
            }

        }

        public static void AddProjectAssignmentNotification(int projectId, string newUser)
        {
            var properProjectName = db.Projects.FirstOrDefault(t => t.Id == projectId).Name;
            var newNotification = new ProjectNotification
            {
                Created = DateTimeOffset.UtcNow.ToLocalTime(),
                ProjectId = projectId,
                Unread = true,
                UserId = newUser,
                NotificationBody = $"You have been assigned to project '{properProjectName}'."
            };
            db.ProjectNotifications.Add(newNotification);
            db.SaveChanges();
        }

        public static void AddProjectUnassignmentNotification(int projectId, string oldUser)
        {
            var properProjectName = db.Projects.FirstOrDefault(t => t.Id == projectId).Name;
            var oldNotification = new ProjectNotification
            {
                Created = DateTimeOffset.UtcNow.ToLocalTime(),
                ProjectId = projectId,
                Unread = true,
                UserId = oldUser,
                NotificationBody = $"You have been unassigned from project '{properProjectName}'."
            };
            db.ProjectNotifications.Add(oldNotification);
            db.SaveChanges();
        }

        public ICollection<ProjectNotification> ListUserProjectNotifications(string userId)
        {
            var readNotifications = db.ProjectNotifications.Where(t => t.UserId == userId).ToList();
            return (readNotifications);
        }

        public ICollection<ProjectNotification> ListUserUnreadProjectNotifications(string userId)
        {
            var unreadNotifications = db.ProjectNotifications.Where(t => t.Unread && t.UserId == userId).ToList();
            return (unreadNotifications);
        }
    }
}