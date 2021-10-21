using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheRentersPlace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["OwnerUsername"] != null)
            {
                return RedirectToAction("Index", "Owners");
            }
            if (Session["TenantUsername"] != null)
            {
                return RedirectToAction("Index", "Tenants");
            }
            if (Session["ManagerUsername"] != null)
            {
                return RedirectToAction("Index", "Managers");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }
    }
}