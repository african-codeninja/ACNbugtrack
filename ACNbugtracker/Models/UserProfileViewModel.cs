using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACNbugtracker.Models
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "Cannot be greater than 50 Characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [MaxLength(20, ErrorMessage= "Cannot be greater than 20 Characters")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Avatar path")]
        public string AvatarUrl { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public HttpPostedFileBase Avatar { get; set; }
    }
}