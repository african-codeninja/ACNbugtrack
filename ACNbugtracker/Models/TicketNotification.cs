using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Models
{
    public class TicketNotification
    {
        //key
        public int Id { get; set; }

        //Foreign Keys
        public int TicketId { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }

        //structure
        public DateTime Created { get; set; }
        public string Subject { get; set; }
        public string NotificationBoody { get; set; }
        public bool Read { get; set; }
        

        //Nav
        public virtual Ticket Ticket { get; set; }
        public ApplicationUser Recipient { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}