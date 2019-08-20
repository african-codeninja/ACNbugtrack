using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Helper
{
    public class NavHelper: CommonHelper
    {
        //Help determine what links I can see in the side nav

        public bool UserCanAddTickets()
        {
            //I am going to switch off the
            switch (this.CurrentRole)
            {
                case SystemRole.Submitter:
                    return true;
                case SystemRole.Admin:
                case SystemRole.ProjectManager:
                case SystemRole.Developer:
                default:
                    return false;
            }
        }
    }
}