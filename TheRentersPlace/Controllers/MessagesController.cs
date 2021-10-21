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
    public class MessagesController : Controller
    {
        private TheRentersPlaceEntities1 db = new TheRentersPlaceEntities1();

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Manager).Include(m => m.Tenant);
            return View(messages.ToList());
        }

        public ActionResult TenantMessages()
        {
            Tenant tenant = db.Tenants.Find(Session["TenantId"]);
            return View(tenant);
        }
        public ActionResult ManagerMessages()
        {
            Manager manager = db.Managers.Find(Session["ManagerId"]);
            return View(manager);
        }
        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult TenantCreate(int? ManagerId, String ManagerUsername)
        {
            ViewBag.ManagerUsername = ManagerUsername;
            ViewBag.ManagerId = ManagerId;
            ViewBag.TenantId = Session["TenantId"];
            ViewBag.SenderUsername = Session["TenantUsername"];
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TenantCreate([Bind(Include = "MessageId,TenantId,ManagerId,SenderUsername,ReceiverUsername,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("TenantMessages");
            }

            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", message.ManagerId);
            ViewBag.TenantId = Session["TenantId"];
            return View(message);
        }

        public ActionResult ManagerCreate(int? TenantId, String TenantUsername)
        {
            ViewBag.TenantUsername = TenantUsername;
            ViewBag.TenantId = TenantId;
            ViewBag.ManagerId = Session["ManagerId"];
            ViewBag.SenderUsername = Session["ManagerUsername"];
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerCreate([Bind(Include = "MessageId,TenantId,ManagerId,SenderUsername,ReceiverUsername,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("ManagerMessages");
            }

            ViewBag.ManagerId = Session["ManagerId"];
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "Username", message.TenantId);
            return View(message);
        }


        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", message.ManagerId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "Username", message.TenantId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MessageId,TenantId,ManagerId,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", message.ManagerId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "Username", message.TenantId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
