﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ACNbugtracker.ViewModels
{
    public class UserViewModel
    {
        [Required(AllowEmptyStrings = false), Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false), EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public HttpPostedFileBase Avatar { get; set; }

        public string ProfilePic { get; set; }
    }
}