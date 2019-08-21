﻿using ACNbugtracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;

namespace ACNbugtracker.Controllers
{
    public class HomeController : Controller
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        //Checks who the user is and displays their information
        public ActionResult Index()
        {
            var users = db.Users.Select(userAttrib => new UserProfileViewModel
            {
                Id = userAttrib.Id,
                FirstName = userAttrib.FirstName,
                LastName = userAttrib.LastName,
                DisplayName = userAttrib.DisplayName,
                AvatarUrl = userAttrib.AvatarUrl,
                Email = userAttrib.Email
            }).ToList();

            return View(users);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your Register page.";

            return View();
        }

        public ActionResult DemoUser()
        {
            return View();
        }
    }
}