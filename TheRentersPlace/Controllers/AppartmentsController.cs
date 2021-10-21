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
    public class AppartmentsController : Controller
    {
        private TheRentersPlaceEntities1 db = new TheRentersPlaceEntities1();

        // GET: Appartments
        public ActionResult Index()
        {
            var appartments = db.Appartments.Include(a => a.Manager);
            return View(appartments.ToList());
        }

        public ActionResult AppartmentList()
        {
            var appartments = db.Appartments.Where(a => a.Status == "Available");
            return PartialView(appartments.ToList());
        }
        public ActionResult AppartmentListWithFilter(String Filter, String Search)
        {
            var appartments = (IQueryable<Appartment>)null;
            Search.Trim();
            switch (Filter)
            {
                case "City":
                    appartments = db.Appartments.Where(a => a.City == Search);
                    break;
                case "Address":
                    appartments = db.Appartments.Where(a => a.CivicAddress == Search);
                    break;
                case "Province":
                    appartments = db.Appartments.Where(a => a.Province == Search);
                    break;
                case "Number of bedrooms":
                    var number = Convert.ToDecimal(Search);
                    appartments = db.Appartments.Where(a => a.NoOfBedrooms == number);
                    break;
                case "Type":
                    appartments = db.Appartments.Where(a => a.Type == Search);
                    break;
                case "Price":
                    var price = Convert.ToDecimal(Search);
                    appartments = db.Appartments.Where(a => a.Price == price);
                    break;
            }       

            return PartialView(appartments.ToList());
        }

        // GET: Appartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appartment appartment = db.Appartments.Find(id);
            if (appartment == null)
            {
                return HttpNotFound();
            }
            return View(appartment);
        }

        // GET: Appartments/Create
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username");
            return View();
        }

        // POST: Appartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppartmentId,ManagerId,CivicAddress,City,Province,NoOfBedrooms,Type,Price,Status,PictureURL")] Appartment appartment)
        {
            if (ModelState.IsValid)
            {
                db.Appartments.Add(appartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", appartment.ManagerId);
            return View(appartment);
        }

        // GET: Appartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appartment appartment = db.Appartments.Find(id);
            if (appartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", appartment.ManagerId);
            return View(appartment);
        }

        // POST: Appartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppartmentId,ManagerId,CivicAddress,City,Province,NoOfBedrooms,Type,Price,Status,PictureURL")] Appartment appartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appartment).State = EntityState.Modified;
                db.SaveChanges();
                if (Session["ManagerUsername"] != null)
                {
                    return RedirectToAction("Index", "Managers");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "Username", appartment.ManagerId);
            return View(appartment);
        }

        // GET: Appartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appartment appartment = db.Appartments.Find(id);
            if (appartment == null)
            {
                return HttpNotFound();
            }
            return View(appartment);
        }

        // POST: Appartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appartment appartment = db.Appartments.Find(id);
            db.Appartments.Remove(appartment);
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
