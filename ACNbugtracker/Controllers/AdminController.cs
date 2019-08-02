using ACNbugtracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACNbugtracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        public ActionResult ManageUserRole()
        {
            return View();
        }

        public ActionResult ManageUsers()
        {
            return View();
        }

        public ActionResult ManageUserProjects()
        {
            return View();
        }

        public ActionResult ManageRoles()
        {
            return View();
        }

        public ActionResult ManageProjects()
        {
            return View();
        }
    }
}