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
    public class TenantsController : Controller
    {
        private TheRentersPlaceEntities1 db = new TheRentersPlaceEntities1();

        // GET: Tenants
        public ActionResult Index()
        {
            List<String> filterOptions = new List<String>();
            filterOptions.Add("Address");
            filterOptions.Add("City");
            filterOptions.Add("Province");
            filterOptions.Add("Number of bedrooms");
            filterOptions.Add("Type");
            filterOptions.Add("Price");
            ViewBag.Filter = new SelectList(filterOptions, "Filter");
            //var appartments = db.Appartments.Where(a => a.Status == "Available");
            return View();
        }

        public ActionResult ListAllTenants()
        {
            return View(db.Tenants.ToList());
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
                var data = db.Tenants.Where(s => s.Username.Equals(username) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["TenantUsername"] = data.FirstOrDefault().Username;
                    Session["TenantId"] = data.FirstOrDefault().TenantId;
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

        // GET: Tenants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // GET: Tenants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenantId,Username,Password,FirstName,LastName,Email")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Tenants.Add(tenant);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenantId,Username,Password,FirstName,LastName,Email")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            db.Tenants.Remove(tenant);
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
