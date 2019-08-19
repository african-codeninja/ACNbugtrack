using ACNbugtracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace ACNbugtracker.Helper
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static void CreateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            //4 cases considered for this case scenarios for notifications

            //1. No Change
            //2. An Assignment - This means that the old AssignedToUserId was null and the current one is not
            //3. An Unassignement - This means that the old AssignedToUserId had a value and the current one is null 
            //4. A reassignment - The Ticket was assigned to someone and it is now assigned to someone else

            //Setting up Boolean variables to determine which case I am in
            var noChange = (oldTicket.AssignedToUserId == newTicket.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(oldTicket.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(newTicket.AssignedToUserId));

            if (noChange)
                return;

            if (assignment)
                GenerateAssignmentNofification(oldTicket, newTicket);
            else if (unassignment)
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            else
            {
                GenerateAssignmentNofification(oldTicket, newTicket);
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            }
        }   
        

        public static void GenerateUnAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were unassigned from Ticket Id {newTicket.Id} on {DateTime.Now}",
                Read = false,
                RecipientId = oldTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBoody = $"Please acknowledge that you have read this notification by marking it",
                TicketId = newTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        public static void GenerateAssignmentNofification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were assigned from Ticket Id {newTicket.Id} on {DateTime.Now}",
                Read = false,
                RecipientId = newTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBoody = $"Please acknowledge that you have read this notification by marking it",
                TicketId = newTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        private static void CreateChangeNotification(Ticket oldTicket, Ticket newTicket)
        {
            var messageBody = new StringBuilder();

            foreach (var property in WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(','))
            {
                var oldValue = oldTicket.GetType().GetProperty(property).GetValue(oldTicket, null).ToString();
                var newValue = newTicket.GetType().GetProperty(property).GetValue(newTicket, null).ToString();

                if (oldValue != newValue)
                {
                    messageBody.AppendLine(new String('-', 45));
                    messageBody.AppendLine($"A Change was made to property: {property}.");
                    messageBody.AppendLine($"The Old value was: {oldValue.ToString()}");
                    messageBody.AppendLine($"The new value is: {newValue.ToString()}");
                }
            }

            if (!string.IsNullOrEmpty(messageBody.ToString()))
            {
                var message = new StringBuilder();
                message.AppendLine($"Changes were made to Ticket Id: {newTicket.Id} on {newTicket.Updated.GetValueOrDefault()}");
                message.AppendLine(messageBody.ToString());
                var senderId = HttpContext.Current.User.Identity.GetUserId();

                var notification = new TicketNotification
                {
                    TicketId = newTicket.Id,
                    Created = DateTime.Now,
                    Subject = $"Ticket Id: {newTicket.Id} has changed",
                    RecipientId = newTicket.AssignedToUserId,
                    SenderId = senderId,
                    NotificationBoody = message.ToString(),
                    Read = false
                };

                db.TicketNotifications.Add(notification);
                db.SaveChanges();
            }

        }
            
    }
}