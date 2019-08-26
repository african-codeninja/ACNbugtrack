using ACNbugtracker.ChartViewModels;
using ACNbugtracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACNbugtracker.Controllers
{
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

        public JsonResult GetJsonFusionData()
        {
            var dataSet = new List<MorrisBarData>();

            foreach (var ticketStatus in db.TicketStatuses.ToList())
            {
                var value = db.TicketStatuses.Find(ticketStatus.Id).Tickets.Count();
                dataSet.Add(new MorrisBarData { label = ticketStatus.Name, value = value });
            }
            return Json(dataSet);
        }
    }

    
}