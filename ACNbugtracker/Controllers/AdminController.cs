using ACNbugtracker.Helper;
using ACNbugtracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACNbugtracker.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();

        // GET: Admin
        public ActionResult UserIndex()
        {
            var users = db.Users.Select(userAttrib => new UserProfileViewModel
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
        //Get Function
        [HttpGet]
        public ActionResult ManageUserRole(string userId)
        {
            //How do i up a DropDownList with Role Information??
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
            
            foreach(var role in rolesHelper.ListUserRoles(userId))
            {
                rolesHelper.RemoveUserFromRole(userId, role);
            }

            //if the incoming role selection IS NOT NULL I want to assign the user to the selected role
            if(!string.IsNullOrEmpty(userRole))
            {
                rolesHelper.AddUserToRole(userId, userRole);
            }

            return RedirectToAction("UserIndex");

        }

        public ActionResult ManageUsers()
        {
            return View();
        }

        

        [HttpGet]
        public ActionResult ManageRoles()
        {
            var users = db.Users.Select(userAttrib => new UserProfileViewModel
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
            if(users != null)
            { 
            //Lets iterate over the incoming list that were selected from the form
            foreach(var userId in users)
            {
                //and remove each of them from whatever role they occupy and only add them back to the selected role
                foreach(var role in rolesHelper.ListUserRoles(userId))
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

        public ActionResult ManageProjects()
        {
            return View();
        }

        public ActionResult ManageUserProjects()
        {
            return View();
        }
    }
}