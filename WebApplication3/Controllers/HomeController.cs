using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private dbReadNLearnEntities db = new dbReadNLearnEntities();
        public ActionResult Index()
        {

            var tbl_coupons = db.Tbl_Coupons.Include(t => t.Tbl_Events);
            return View(tbl_coupons.ToList());

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult BookList()
        {

            var tbl_Products = db.Tbl_Products.Include(t => t.Tbl_Categories);
            return View(tbl_Products.ToList());
        }
        public ActionResult login()
        {


            return View();
        }

    }
}