using ACNbugtracker.Models;
using ACNbugtracker.Helper;
using ACNbugtracker.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Helper
{
    public class LinkHelper : CommonHelper
    {
        public bool UserCanEditTicket(Ticket ticket)
        {
            switch (this.CurrentRoles)
            {
                case "Admin":
                    return true;
                case "ProjectManager":
                    return CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Developer":
                    return ticket.AssignedToUserId == CurrentUser.Id;
                case "Submitter":
                    return ticket.OwnerUserId == CurrentUser.Id;
                default:
                    return false;
            }
        }
    }
}