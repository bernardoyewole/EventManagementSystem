using EMSBLL;
using EMSENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index(int eventId)
        {
            RegistrationService rs = new RegistrationService();
            var registrations = rs.GetRegistrations(eventId);
            TempData["eventId"] = eventId;
            return View(registrations);
        }

        // Returns the view page to create registration
        public ActionResult CreateRegistration()
        {
            EventService es = new EventService();
            var events = es.GetEvents(); // Assuming you have a method to get user's events

            var eventItems = events.Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.EventId.ToString()
            }).ToList();

            ViewBag.EventItems = eventItems; // You can also pass this to your model
            return View();
        }

        // Handles creating a registration in registration repository
        [HttpPost]
        public ActionResult CreateRegistration(Registration registration)
        {
            RegistrationService rs = new RegistrationService();

            if (rs.AddRegistration(registration))
            {
                return RedirectToAction("Index", new { eventId = registration.EventId, name = "eventId" });
            }
            return RedirectToAction("CreateRegistration");
        }

        // returns view page to edit registration
        public ActionResult EditRegistration(int registrationId)
        {
            RegistrationService rs = new RegistrationService();
            int eventId = (int)TempData["eventId"];
            var registration = rs.GetRegistrations(eventId).Find(x => x.RegistrationId == registrationId);
            return View(registration);
        }

        // Handles user inputs to edit registration in repository
        [HttpPost]
        public ActionResult EditRegistration(Registration registration)
        {
            RegistrationService rs =new RegistrationService();

            if (rs.UpdateRegistration(registration))
            {
                ViewBag.Message = "Registration updated successfully";
            }
            else
            {
                ViewBag.Message = "Check your inputs and try again";
            }

            return RedirectToAction("Index", new { eventId = registration.EventId });
        }

        public ActionResult DeleteRegistration(int registrationId)
        {
            RegistrationService rs = new RegistrationService();
            var eventId = (int)TempData["eventId"];

            if (rs.DeleteRegistration(registrationId))
            {
                return RedirectToAction("Index", new {eventId = eventId});
            }

            return null;
        }
    }
}