﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACNbugtracker.Helper;
using ACNbugtracker.Models;
using Microsoft.AspNet.Identity;

namespace ACNbugtracker.Controllers
{
    [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();

        //GET: Projects
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        //Get: My Projects
        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        public ActionResult MyProjectsIndex()
        {
            return View(projectsHelper.ListUserProjects(User.Identity.GetUserId()));
        }

    
        //    return View("Index", myTickets);
        //}

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            //Give my details view a Multiselect of available PM's
            //Keep an eye out for 'Magic Strings' throughout your application..
            //Magic Strings are hard coded strings that should be handled differently...

            
            var allProjectManagers = rolesHelper.UsersInRole("ProjectManager");
            var currentProjectManagers = projectsHelper.UserInRoleOnProject(project.Id, "ProjectManager");
            ViewBag.ProjectManagers = new MultiSelectList(allProjectManagers, "Id", "FullNameWithEmail", currentProjectManagers);

            var allSubmitters = rolesHelper.UsersInRole("Submitter");
            var currentSubmitters = projectsHelper.UserInRoleOnProject(project.Id, "Submitter");
            ViewBag.Submitters = new MultiSelectList(allSubmitters, "Id", "FullNameWithEmail", currentSubmitters);

            var allDevelopers = rolesHelper.UsersInRole("Developer");
            var currentDevelopers = projectsHelper.UserInRoleOnProject(project.Id, "Developer");
            ViewBag.Developers = new MultiSelectList(allDevelopers, "Id", "FullNameWithEmail", currentDevelopers);

            return View(project);
        }

        // GET: Projects/Details/5
        public ActionResult RoleDetailsView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            //Give my details view a Multiselect of available PM's
            //Keep an eye out for 'Magic Strings' throughout your application..
            //Magic Strings are hard coded strings that should be handled differently...


            var allProjectManagers = rolesHelper.UsersInRole("ProjectManager");
            var currentProjectManagers = projectsHelper.UserInRoleOnProject(project.Id, "ProjectManager");
            ViewBag.ProjectManagers = new MultiSelectList(allProjectManagers, "Id", "FullNameWithEmail", currentProjectManagers);

            var allSubmitters = rolesHelper.UsersInRole("Submitter");
            var currentSubmitters = projectsHelper.UserInRoleOnProject(project.Id, "Submitter");
            ViewBag.Submitters = new MultiSelectList(allSubmitters, "Id", "FullNameWithEmail", currentSubmitters);

            var allDevelopers = rolesHelper.UsersInRole("Developer");
            var currentDevelopers = projectsHelper.UserInRoleOnProject(project.Id, "Developer");
            ViewBag.Developers = new MultiSelectList(allDevelopers, "Id", "FullNameWithEmail", currentDevelopers);

            return View(project);
        }


        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTime.UtcNow;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
