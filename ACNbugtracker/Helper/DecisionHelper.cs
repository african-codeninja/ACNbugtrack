using ACNbugtracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Helper
{
    public static class TicketDecisionHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserRolesHelper rolesHelper = new UserRolesHelper();
        private static ProjectsHelper projectHelper = new ProjectsHelper();

        //public static bool TicketsDetailsViewableByUser(int ticketId)
        //{
        //    var userId = HttpContext.Current.User.Identity.GetUserId();

        //    var roleName = rolesHelper.ListUserRoles(userId).FirstOrDefault();
        //    var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName;

        //    switch (systemRole)
        //    {
        //        case systemRole.Admin:
        //            break;
        //        case systemRole.ProjectManager:
        //            break;
        //        case systemRole.Developer:
        //            break;
        //        case systemRole.Submitter:
        //            break;
        //    }

        //    return true;

        //}

        //public static bool TicketsDetailsViewableByUser( int ticketId)
        //{
        //    var userId = HttpContext.Current.User.Identity.GetUserId();

        //    var roleName = rolesHelper.ListUserRoles(userId).FirstOrDefault();
        //    var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName);

        //    switch (systemRole)
        //    {
        //        case systemRole.Admin:
        //            break;
        //        case systemRole.ProjectManager:
        //            break;
        //        case systemRole.Developer:
        //            break;
        //        case systemRole.Submitter:
        //            break;
        //    }

        //    return true;

        //}

        public static bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return (ticket.AssignedToUserId == userId);
                case "Submitter":
                    return (ticket.OwnerUserId == userId);
                case "ProjectManager":
                    var myProjects = projectHelper.ListUserProjects(userId);
                    foreach (var project in myProjects)
                    {
                        foreach (var projticket in project.Tickets)
                        {
                            if (projticket.Id == ticket.Id)
                                return true;
                        }
                    }
                    return false;

                case "Admin":
                    return true;
                default:
                    return false;
            }
        }
    }    

            ////Based on my role....can I edit this
            //if (HttpContext.Current.User.IsInRole("Developer") && ticket.AssignedToUserId == userId)
            //    allowed = true;
            //else if (User.IsInRole("Submitter") && ticket.OwnerUserId == userId)
            //    allowed = true;
            //else if (User.IsInRole("ProjectManager"))
            //{
            //    //if this ticket is on a project that I am on then can I edit this ticket
            //}
            //else
            //{
            //    allowed = true;
            //}


            

        

        //public static bool TicketTypeIsEditableByUser(string userId, int ticketId)
        //{
        //    var userId = HttpContext.Current.User.Identity.GetUserId();

        //    var roleName = rolesHelper.ListUserRoles(userId).FirstOrDefault();
        //    var systemRole = (systemRole)Enum.Parse(typeof(String);

        //    switch (systemRole)
        //    {
        //        case systemRole.Admin:
        //            break;
        //        case systemRole.ProjectManager:
        //            break;
        //        case systemRole.Developer:
        //            break;
        //        case systemRole.Submitter:
        //            break;
        //    }

        //    return true;

        //}

    //    public static bool TicketsTypeIsEditableByUser(string userId, int ticketId)
    //    {
    //        var userId = HttpContext.Current.User.Identity.GetUserId();

    //        var roleName = rolesHelper.ListUserRoles(userId).FirstOrDefault();
    //        var systemRole = (systemRole)Enum.Parse(typeof(String);

    //        switch (systemRole)
    //        {
    //            case systemRole.Admin:
    //                break;
    //            case systemRole.ProjectManager:
    //                break;
    //            case systemRole.Developer:
    //                break;
    //            case systemRole.Submitter:
    //                break;
    //        }

    //        return true;

    //    }

    //}

}