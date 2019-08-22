using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ACNbugtracker.Helper;
using ACNbugtracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ACNbugtracker.Controllers
{
    public class TicketsController : Controller
    {
        public static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();
        private NotificationHelper notificationHelper = new NotificationHelper();
        private TicketDecisionHelper ticketDecisionHelper = new TicketDecisionHelper();
        private HistoryHelper historyHelper = new HistoryHelper();

        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        // GET: Tickets
        public ActionResult Dashboard(int id)
        {
            var tickets = db.Tickets.Find(id);

            if(tickets == null)
            {
                return HttpNotFound();
            }

            return View(tickets);
        }

        [Authorize(Roles = "Admin, ProjectManager, Developer, Submitter")]
        //Creating MyIndex view that is only filled with data that belongs to a particular role
        public ActionResult MyIndex()
        {
            //First capture the Id of the logged in user
            var userId = User.Identity.GetUserId();
            //Then get the role they occupy
            var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();

            var myTickets = new List<Ticket>();
            //Now base one the logged in person role we will push different data into the view
            switch(myRole)
            {
                case "Developer":
                    myTickets = db.Tickets.Where(t => t.AssignedToUserId == userId).ToList();
                    break;
                case "Submitter":
                    myTickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();
                    break;
                case "ProjectManager":
                    myTickets = db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).ToList();
                    break;
                case "Admin":
                    var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
                    return View(tickets.ToList());
                    break;
            }

            return View("Index", myTickets);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Find(id);

            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]
        public ActionResult Create()
        {
            //var userId = User.Identity.GetUserId();
            var myProjects = projectsHelper.ListUserProjects(User.Identity.GetUserId());
            //Create a create view only for the tickets the submitter is on when we did the Manage projects
            ViewBag.ProjectId = new SelectList(myProjects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");            
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,TicketTypeId,TicketPriorityId,Title,Description")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Created = DateTime.Now;
                ticket.OwnerUserId = User.Identity.GetUserId();//whoever is logged in
                ticket.TicketStatusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "New / Unassigned").Id;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, Developer, Submitter, ProjectManager")]
        public ActionResult Edit(int? id)
        {         
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            

            Ticket ticket = db.Tickets.Find(id);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();

            var projectDevelopers = projectsHelper.UserWithRoleOnProject(ticket.ProjectId, "Developer");

            if (TicketDecisionHelper.TicketIsEditableByUser(ticket))
            {
                ViewBag.AssignedToUserId = new SelectList(projectDevelopers, "Id", "FirstName", ticket.AssignedToUserId);
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
                ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
                ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
                ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
                return View(ticket);
            }
            else
            {
                return RedirectToAction("AccessViolation", "Admin");
            }
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,OwnerUserId,AssignedToUserId,Title,Description,Created,Updated")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //Go out to the DB and get a copy of the Ticket before it is changed
                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);

                ticket.Updated = DateTime.UtcNow;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                //Now Call the NotificationHelper to determine if a Notification needs to be created
                NotificationHelper.CreateAssignmentNotification(oldTicket, ticket);
                HistoryHelper.RecordHistory(oldTicket, ticket);

                return RedirectToAction("MyIndex");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("MyIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Get Assignticket page
        public ActionResult AssignTicket(int? id)
        {
            //var myProjects = projectsHelper.ListUserProjects(User.Identity.GetUserId());
            ////Create a create view only for the tickets the submitter is on when we did the Manage projects
            //ViewBag.ProjectId = new SelectList(myProjects, "Id", "Name");
            //ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            //ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            //return View();

            UserRolesHelper helper = new UserRolesHelper();

            var ticket = db.Tickets.Find(id);

            var users = helper.UsersInRole("DEVELOPER").ToList();

            ViewBag.AssignedToUserId = new SelectList(users, "id", "FullName", ticket.AssignedToUserId);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTicket(Ticket model)
        {
            var ticket = db.Tickets.Find(model.Id);

            ticket.AssignedToUserId = model.AssignedToUserId;

            db.SaveChanges();

            var callbackUrl = Url.Action("Details", "Tickets", new { id = ticket.Id }, protocol: Request.Url.Scheme);

            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                ApplicationUser user = db.Users.Find(model.AssignedToUserId);

                msg.Body = "You Have been assigned a new Ticket." + Environment.NewLine +
                            "Please click the following link to view the detail " +
                            "<a href=\"" + callbackUrl + "\">New Ticket</a>";

                msg.Destination = user.Email;
                msg.Subject = "Invite to Household";

                await ems.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }

            return RedirectToAction("MyIndex");
        }

    }
}
