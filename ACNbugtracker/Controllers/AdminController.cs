using ACNbugtracker.Helper;
using ACNbugtracker.Models;
using ACNbugtracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ACNbugtracker.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        // GET: Admin
        public ActionResult UserIndex()
        {
            var users = db.Users.Select(userAttrib => new Models.UserProfileViewModel
            {
                Id = userAttrib.Id,
                FirstName = userAttrib.FirstName,
                LastName = userAttrib.LastName,
                DisplayName = userAttrib.DisplayName,
                AvatarUrl = userAttrib.AvatarUrl,
                Email = userAttrib.Email
            }).ToList();

            return View(users);
        }

        // GET: Admin2       
        public ActionResult UserIndex2()
        {
            var roles = db.Roles.ToList();
            var projects = db.Projects;
            var users = db.Users.Select(u => new UserIndexViewModel
            {
                Id = u.Id,
                FullName = u.LastName + ", " + u.FirstName,
                Email = u.Email,
                AvatarUrl = u.AvatarUrl
            }).ToList();

            foreach (var user in users)
            {
                user.CurrentRole = new SelectList(roles, "Name", "Name", rolesHelper.ListUserRoles(user.Id).FirstOrDefault());
                user.CurrentProjects = new MultiSelectList(projects, "Id", "Name", projectHelper.ListUserProjects(user.Id).Select(p => p.Id));
            }

            return View(users);
        }

        public ActionResult Permissions()
        {
            return View();
        }
    
        //Get Function
        [HttpGet]
        public ActionResult ManageUserRole(string userId)
        {
            //How do i SPIN up a DropDownList with Role Information??
            //new SelectList("[First Selection]     The List of Data push into the control", 
            //               "[Second selection]    The Column that will be used to communicate my selection(s) to the post",
            //               "[Third selection]     The Column that we show the user inside the control",
            //               "[Fourth Selection]    If they already occupy a role.... show this instead of nothing


            var currentRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.UserRole = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string userId, string userRole)
        {
            ///This is where I will be using the UserRolesHelper class to make sure my user occupies the proper role
            ///The first thing I want to do is to make sure I remove the user from ALL ROLES they may occupy

            foreach (var role in rolesHelper.ListUserRoles(userId))
            {
                rolesHelper.RemoveUserFromRole(userId, role);
            }

            //if the incoming role selection IS NOT NULL I want to assign the user to the selected role
            if (!string.IsNullOrEmpty(userRole))
            {
                rolesHelper.AddUserToRole(userId, userRole);
            }

            return RedirectToAction("UserIndex");

        }

        public ActionResult ManageUsers()
        {
            var users = db.Users.Select(userAttrib => new Models.UserProfileViewModel
            {
                Id = userAttrib.Id,
                FirstName = userAttrib.FirstName,
                LastName = userAttrib.LastName,
                DisplayName = userAttrib.DisplayName,
                AvatarUrl = userAttrib.AvatarUrl,
                Email = userAttrib.Email

            }).ToList();

            return View(users);
        }



        [HttpGet]
        public ActionResult ManageRoles()
        {
            var users = db.Users.Select(userAttrib => new Models.UserProfileViewModel
            {
                Id = userAttrib.Id,
                FirstName = userAttrib.FirstName,
                LastName = userAttrib.LastName,
                DisplayName = userAttrib.DisplayName,
                AvatarUrl = userAttrib.AvatarUrl,
                Email = userAttrib.Email
            }).ToList();

            ViewBag.Users = new MultiSelectList(db.Users.ToList(), "Id", "Email");
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "Name", "Name");

            return View(users);

        }
        [HttpPost]
        public ActionResult ManageRoles(List<string> users, string roleName)
        {
            if (users != null)
            {
                //Lets iterate over the incoming list that were selected from the form
                foreach (var userId in users)
                {
                    //and remove each of them from whatever role they occupy and only add them back to the selected role
                    foreach (var role in rolesHelper.ListUserRoles(userId))
                    {
                        rolesHelper.RemoveUserFromRole(userId, role);
                    }

                    //then only to add the user back to the selected role
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        rolesHelper.AddUserToRole(userId, roleName);
                    }

                }
            }
            return RedirectToAction("ManageRoles");
        }
        [HttpGet]
        public ActionResult ManageProjects(string userId)
        {
            //var users = db.Users.Find(userId);

            //var users = db.Users.Select(userAttrib => new UserProfileViewModel
            //{
            //    Id = userAttrib.Id,
            //    FirstName = userAttrib.FirstName,
            //    LastName = userAttrib.LastName,
            //    DisplayName = userAttrib.DisplayName,
            //    AvatarUrl = userAttrib.AvatarUrl,
            //    Email = userAttrib.Email
            //}).ToList();

            var myProjects = projectHelper.ListUserProjects(userId).Select(p => p.Id);
            ViewBag.Projects = new MultiSelectList(db.Projects.ToList(), "Id", "Name", myProjects);
            return View();         
        }

        //Get Function
        public ActionResult ManageUserProjects(string userId, string projectId)
        {
            var myProjects = projectHelper.ListUserProjects(userId).Select(p => p.Id);
            ViewBag.Projects = new MultiSelectList(db.Projects.ToList(), "Id", "Name", myProjects);
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserProjects(List<int> projects, string userId)
        {
            foreach (var project in projectHelper.ListUserProjects(userId).ToList())
            {
                ProjectsHelper.RemoveUserFromProject(userId, project.Id);
            }
            if (projects != null)
            {
                foreach (var projectId in projects)
                {
                    ProjectsHelper.AddUserToProject(userId, projectId);
                }
            }
            return RedirectToAction("UserIndex");

        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(int projectId, List<string> ProjectManagers, List<string> Developers, List<string> Submitters)
        {
            //Step 1: Remove all users from the project
            foreach (var user in projectHelper.UsersOnProject(projectId).ToList())
            {
                ProjectsHelper.RemoveUserFromProject(user.Id, projectId);
            }

            //Step 2: Adds back all the selected PM's
            if (ProjectManagers != null)
            {
                foreach (var projectManagerId in ProjectManagers)
                {
                    ProjectsHelper.AddUserToProject(projectManagerId, projectId);
                }
            }

            //Step 3: Adds all the selected Developers
            if (Developers != null)
            {
                foreach (var developerId in Developers)
                {
                    ProjectsHelper.AddUserToProject(developerId, projectId);
                }
            }

            //Step 4:Adds back all the selected Submittters
            if (Submitters != null)
            {
                foreach (var submitterId in Submitters)
                {
                    ProjectsHelper.AddUserToProject(submitterId, projectId);
                }
            }

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

        public ActionResult RemoveProjectUser(string userId, int projectId)
        {
            ProjectsHelper.RemoveUserFromProject(userId, projectId);

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }
    }
}

