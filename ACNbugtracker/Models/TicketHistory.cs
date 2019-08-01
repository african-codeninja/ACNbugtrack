using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Models
{
    public class TicketHistory
    {
        //key
        public int Id { get; set; }

        //Foreign Keys
        public int TicketId { get; set; }
        public string UserId { get; set; }

        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Updated { get; set; }

        //Nav
        public virtual Ticket Ticket{ get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}