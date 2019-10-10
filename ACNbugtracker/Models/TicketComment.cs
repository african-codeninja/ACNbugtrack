using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Models
{
    public class TicketComment
    {
        [Display(Name = "Ticket Type Id")]
        public int Id { get; set; }
        [Display(Name = "Ticket Owner Id")]
        public int TicketId { get; set; }

        [Display(Name = "Ticket Owner Id")]
        public string AuthorId { get; set; }
        [Display(Name = "Comment")]
        public string CommentBody { get; set; }
        [Display(Name = "Ticket Comment Create")]
        public DateTime Created { get; set; }

        //Nav
        public virtual Ticket Ticket{ get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}