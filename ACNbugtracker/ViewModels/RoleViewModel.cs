﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACNbugtracker.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
