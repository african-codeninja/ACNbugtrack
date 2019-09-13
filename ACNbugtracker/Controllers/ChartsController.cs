using ACNbugtracker.ChartViewModels;
using ACNbugtracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACNbugtracker.Controllers
{
    [Authorize]
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Charts
        public JsonResult GetJsonMorrisData()
        {
            var dataSet = new List<MorrisBarData>();

            foreach(var ticketStatus in db.TicketStatuses.ToList())
            {
                var value = db.TicketStatuses.Find(ticketStatus.Id).Tickets.Count();
                dataSet.Add(new MorrisBarData { label = ticketStatus.Name, value = value });               
            }
            return Json(dataSet);
        }

        public JsonResult GetJsonFusionDataTypes()      
        {
            var dataSet = new List<FusionBarData>();

            foreach (var tickettypes in db.TicketTypes.ToList())
            {
                var value = db.TicketStatuses.Find(tickettypes.Id).Tickets.Count();
                dataSet.Add(new FusionBarData { label = tickettypes.Name, value = value.ToString()});
            }
            return Json(dataSet);
        }

        public JsonResult GetJsonFusionData()
        {
            var dataSet = new List<FusionBarData>();

            foreach (var ticketStatus in db.TicketStatuses.ToList())
            {
                var value = db.TicketStatuses.Find(ticketStatus.Id).Tickets.Count();
                dataSet.Add(new FusionBarData { label = ticketStatus.Name, value = value.ToString()});
            }
            return Json(dataSet);
        }

        public JsonResult GetJsonFusionProjectUsers()
        {
            var dataSet = new List<FusionBarData>();

            foreach (var projects in db.Projects.ToList())
            {
                var value = db.Projects.Find(projects.Id).Users.Count();
                dataSet.Add(new FusionBarData { label = projects.Name, value = value.ToString()});
            }
            return Json(dataSet);
        }

        public JsonResult GetJsonFusionDataRoles()
        {
            var dataSet = new List<FusionBarData>();

            foreach (var roles in db.Roles.ToList())
            {
                var value = db.Roles.Find(roles.Id).Users.Count();
                dataSet.Add(new FusionBarData { label = roles.Name, value = value.ToString() });
            }
            return Json(dataSet);
        }
    }

    
}