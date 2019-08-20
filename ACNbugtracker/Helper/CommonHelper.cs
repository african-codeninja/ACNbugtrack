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
        public static ApplicationDbContext db = new ApplicationDbContext();
        protected UserRolesHelper RolesHelper = new UserRolesHelper();
        protected ProjectsHelper ProjectsHelper = new ProjectsHelper();     
        protected ApplicationUser CurrentUser = null;
        protected SystemRole CurrentRole = SystemRole.None;

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId != null)
                CurrentUser = db.Users.Find(userId);

            var stringRole = RolesHelper.ListUserRoles(userId).FirstOrDefault();

            if (!string.IsNullOrEmpty(stringRole))
                CurrentRole = (SystemRole)Enum.Parse(typeof(SystemRole), stringRole);

        }

    }
}