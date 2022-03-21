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
using System.Data.SqlClient;


namespace WebApplication3.Controllers
{
    public class CouponsController : Controller
    {
        private dbReadNLearnEntities db = new dbReadNLearnEntities();
        // GET: Coupons
        public ActionResult Index()
        {
            //DataTable dt = new DataTable();
            //string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            //using (SqlConnection cn = new SqlConnection(str))
            //{

            //    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Coupons"))
            //    {
            //        cmd.Connection = cn;
            //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //        {
            //            da.Fill(dt);
            //        }
            //    }
            //}
            //return View(dt);

            var tbl_coupons = db.Tbl_Coupons.Include(t => t.Tbl_Events);
            return View(tbl_coupons.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.event_id = new SelectList(db.Tbl_Events, "event_id", "event_name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(CouponClass data)
        {
            if (ModelState.IsValid)
            {
                CouponLayer cgen = new CouponLayer();
                cgen.RandomString(8);
                string query = "insert into Tbl_Coupons (coupon_name,event_id,discount_val,couponcode) values('" + data.coupon_name + "', '" + data.event_id + "','" + data.discount_val + "', '" + cgen.RandomString(8) + "')";
                SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = query;

                int res = cmd.ExecuteNonQuery();

                cn.Close();

                return RedirectToAction("Index");
            }

            ViewBag.event_id = new SelectList(db.Tbl_Events, "event_id", "event_name", data.event_id);
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CouponLayer obj = new CouponLayer();
            List<CouponLayer> colist = new List<CouponLayer>();
            CouponClass data = obj.colist.Single(x => x.coupon_id == id);

            ViewBag.event_id = new SelectList(db.Tbl_Events, "event_id", "event_name", data.event_id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CouponClass cpclass)
        {
            if (ModelState.IsValid)
            {
                CouponLayer obj = new CouponLayer();
                obj.updatecoupon(cpclass);
                return RedirectToAction("Index");
            }

            ViewBag.event_id = new SelectList(db.Tbl_Events, "event_id", "event_name", cpclass.event_id);
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Coupons tbl_Coupons = db.Tbl_Coupons.Find(id);
            if (tbl_Coupons == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Coupons);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Coupons tbl_Coupons = db.Tbl_Coupons.Find(id);
            db.Tbl_Coupons.Remove(tbl_Coupons);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}