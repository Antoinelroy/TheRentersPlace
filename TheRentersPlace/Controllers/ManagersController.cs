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
    public class ManagersController : Controller
    {
        private TheRentersPlaceEntities1 db = new TheRentersPlaceEntities1();

        // GET: Managers
        public ActionResult Index()
        {
            Manager manager = db.Managers.Find(Session["ManagerId"]);
            return View(manager);
        }

        public ActionResult ListAllManagers()
        {
            return View(db.Managers.ToList());
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String username, String password)
        {
            if (ModelState.IsValid)
            {
                var data = db.Managers.Where(s => s.Username.Equals(username) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["ManagerUsername"] = data.FirstOrDefault().Username;
                    Session["ManagerId"] = data.FirstOrDefault().ManagerId;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerId,Username,Password,FirstName,LastName,Email")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("ListAllManagers");
            }

            return View(manager);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerId,Username,Password,FirstName,LastName,Email")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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
