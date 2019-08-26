using ACNbugtracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Helper
{
    public abstract class CommonHelper
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected UserRolesHelper RolesHelper = new UserRolesHelper();
        protected ProjectsHelper ProjectsHelper = new ProjectsHelper();
        protected ApplicationUser CurrentUser = null;
        protected String CurrentRoles = "";
        protected SystemRole CurrentRole = SystemRole.None;

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            //if (userId != null)
            //    CurrentUser = db.Users.Find(userId);

            //var stringRole = RolesHelper.ListUserRoles(userId).FirstOrDefault();

            //if (!string.IsNullOrEmpty(stringRole))
            //    CurrentRole = (SystemRole)Enum.Parse(typeof(SystemRole), stringRole);
            CurrentUser = db.Users.Find(userId);
            CurrentRoles = RolesHelper.ListUserRoles(CurrentUser.Id).FirstOrDefault();
        }

    }
}