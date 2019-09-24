using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACNbugtracker.Models;

namespace ACNbugtracker.ViewModels
{
    public class ProfileViewModel
    {
        public IndexViewModel IndexViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
    }
}