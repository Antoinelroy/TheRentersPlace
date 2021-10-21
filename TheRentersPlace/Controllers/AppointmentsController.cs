using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheRentersPlace.Models;

namespace TheRentersPlace.Controllers
{
    public class AppointmentsController : Controller
    {
        private TheRentersPlaceEntities1 db = new TheRentersPlaceEntities1();

        // GET: Appointments
        public ActionResult Index()
        { 
            var appointments = db.Appointments.Include(a => a.Appartment).Include(a => a.Manager).Include(a => a.Tenant);
            return View(appointments.ToList());
        }
        public ActionResult TenantAppointments()
        {
            Tenant tenant = db.Tenants.Find(Session["TenantId"]);
            return View(tenant);
        }
        public ActionResult ManagerAppointments()
        {
            Manager manager = db.Managers.Find(Session["ManagerId"]);
            return View(manager);
        }
        // GET: Appointments/Details/5
        public ActionResult Details(int? AppartmentId, int? TenantId, int? ManagerId)
        {
            if (AppartmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(AppartmentId, TenantId, ManagerId);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create(int? AppartmentId, int? ManagerId, int? TenantId)
        {
            ViewBag.AppartmentId = AppartmentId;
            ViewBag.ManagerId = ManagerId;
            ViewBag.TenantId = TenantId;
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppartmentId,TenantId,ManagerId,Date,Time,Purpose,Note")] Appointment appointment)
        {
            ViewBag.AppartmentId = appointment.AppartmentId;
            ViewBag.ManagerId = appointment.ManagerId;
            ViewBag.TenantId = appointment.TenantId;
            if (ModelState.IsValid)
            {
                String time = appointment.Time;
                String purpose = appointment.Purpose;
                TimeSpan validTime;
                if (TimeSpan.TryParse(time, out validTime))
                {
                    if(purpose.Length != 0)
                    {
                        if (db.Appointments.Find(appointment.AppartmentId, appointment.ManagerId, appointment.TenantId) == null)
                        {
                            db.Appointments.Add(appointment);
                            db.SaveChanges();
                            if (Session["TenantId"] != null)
                            {
                                return RedirectToAction("TenantAppointments");
                            }
                            else if (Session["ManagerId"] != null)
                            {
                                return RedirectToAction("ManagerAppointments");
                            }
                            return RedirectToAction("Index");
                        } else
                        {
                            ViewBag.DuplicateAppointment = "You already have an appointment for this appartment. Please check the list of your appointments";
                            return View(appointment);
                        }
                        
                    } else
                    {
                        ViewBag.Purpose = "Please enter the purpose of this appointment";
                        return View(appointment);
                    }
                }
                ViewBag.Message = "Invalid Time entered. Must use format: hh:mm";
                return View(appointment);
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? AppartmentId, int? TenantId, int? ManagerId)
        {
            if (AppartmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(AppartmentId, TenantId, ManagerId);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppartmentId = new SelectList(db.Appartments, "AppartmentId", "CivicAddress", appointment.AppartmentId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", appointment.ManagerId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "Username", appointment.TenantId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppartmentId,TenantId,ManagerId,Date,Time,Purpose,Note")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppartmentId = new SelectList(db.Appartments, "AppartmentId", "CivicAddress", appointment.AppartmentId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", appointment.ManagerId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "Username", appointment.TenantId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? AppartmentId, int? TenantId, int? ManagerId)
        {
            if (AppartmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(AppartmentId, TenantId, ManagerId);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int AppartmentId, int TenantId, int ManagerId)
        {
            Appointment appointment = db.Appointments.Find(AppartmentId, TenantId, ManagerId);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            if (Session["TenantId"] != null)
            {
                return RedirectToAction("TenantAppointments", "Appointments");
            } else if(Session["ManagerId"] != null)
            {
                return RedirectToAction("ManagerAppointments", "Appointments");
            }
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
