using EMSBLL;
using EMSENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            EventService es = new EventService();
            var events = es.GetEvents();
            return View(events);
        }

        // Returns the create event view page
        public ActionResult CreateEvent()
        {
            return View();
        }

        // Handles creating new event
        [HttpPost]
        public ActionResult CreateEvent(Event @event)
        {
            EventService es = new EventService();
            if (es.AddEventService(@event))
            {
                ViewBag.Message = "Event added successfully";
            } else
            {
                ViewBag.Message = "Please, check your inputs and try again";
            }

            return View();
        }

        // Returns the view page to edit event
        public ActionResult EditEvent(int eventId)
        {
            EventService es = new EventService();
            var @event = es.GetEvents().Find(x =>  x.EventId == eventId);
            return View(@event);
        }

        // Handles editing an event
        [HttpPost]
        public ActionResult EditEvent(Event @event)
        {
            EventService es = new EventService();

            if (es.UpdateEventService(@event))
            {
                ViewBag.Message = "Event updated successfully";
            } else
            {
                ViewBag.Message = "Please, check your inputs and try again";
            }

            return View();
        }

        public ActionResult DeleteEvent(int eventId)
        {
            EventService es = new EventService();
            if (es.DeleteEventService(eventId))
            {
                return RedirectToAction("Index");
            }
            return null;
        }
    }
}