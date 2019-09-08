using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Models
{
    public class EmailNotification
    {
        //Primary Key
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        //Navigation to parent
        public virtual ApplicationUser User { get; set; }
    }
}