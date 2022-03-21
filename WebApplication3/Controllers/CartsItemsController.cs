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
    public class CartsItemsController : Controller
    {
        private dbReadNLearnEntities db = new dbReadNLearnEntities();
        // GET: CartsItems
        public ActionResult Index()
        {
            return View();
        }
    
        public ActionResult Carts()
        {
            List<Tbl_Cart_Items> data = new List<Tbl_Cart_Items>();
            int a = Convert.ToInt32(Session["user_id"]);
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Cart_Items where user_id ='" + a + "'"))
                {
                    cmd.Connection = cn;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                ////var avg = data.Sum(x => x.product_price);
                ////ViewBag.Sumall = avg;
                //CouponClass co = new CouponClass();
                //decimal ab = 0.5m;
                //co.discount_val = ab;
                //var dis = co.discount_val;
                //dt.Compute("Sum(total_val)", string.Empty);
                //Convert.ToDecimal(dt.Compute("Sum(total_val)", string.Empty));
                //ViewBag.discount = (Convert.ToDecimal(dt.Compute("Sum(total_val)", string.Empty)) - (Convert.ToDecimal(dt.Compute("Sum(total_val)", string.Empty)) * Convert.ToDecimal(Session["discount_val"])));

                return View(dt);
            }

        }
        [HttpGet]
        public ActionResult CartDetails(int id)
        {
            Tbl_Products productModel = new Tbl_Products();

            using (dbReadNLearnEntities db = new dbReadNLearnEntities())
            {
                productModel = db.Tbl_Products.Where(x => x.product_id == id).FirstOrDefault();
              
            }
     
            return View(productModel);
        }
        [HttpPost]
        public ActionResult CartDetails(CartClass data)
        {

            if (ModelState.IsValid)
            {
                decimal tval = (Convert.ToDecimal(data.product_price) * Convert.ToDecimal(data.product_qty));
                string query = "insert into Tbl_Cart_Items (user_id,product_id,product_qty,product_price,product_name,total_val) values('" + Session["user_id"] + "', '" + data.product_id + "','" + data.product_qty + "', '" + data.product_price + "', '"+data.product_name+"','"+tval+"')";
                string qry = "update Tbl_Products set product_qty = (product_qty-"+data.product_qty+")  where product_id="+data.product_id+"";
                SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd1 = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = query;

                cmd1.Connection = cn;
                cmd1.CommandText = qry;

                int res = cmd.ExecuteNonQuery();

                int res1 = cmd1.ExecuteNonQuery();

           

                cn.Close();

                return RedirectToAction("Carts");
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CartLayer obj = new CartLayer();
            obj.deletecart(id);
            return RedirectToAction("Carts");
        }
        //[HttpPost]
        //private bool ApplyCouponCode(Tbl_Coupons coupdata, string couponcode)
        //{
        //    // Assigns the coupon code to the shopping cart
        //    coupdata.couponcode = couponcode;
        //    coupdata.Save();

        //    // Returns whether the code is valid (i.e. whether the coupon code applies any discount or is empty)
        //    return coupdata.HasUsableCoupon;
        //}

        [HttpGet]
        public ActionResult coupon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult verifycoupon(Tbl_Coupons coups)
        {
       
                    SqlConnection con = new SqlConnection();
                    SqlCommand com = new SqlCommand();
                    SqlDataReader dr;
                    con.ConnectionString = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

                    con.Open();
                    com.Connection = con;
                    com.CommandText = "select * from Tbl_coupons where couponcode = '" + coups.couponcode + "'";
                    dr = com.ExecuteReader();
                    string coupv = coups.couponcode;
                    bool c = dr.Read();
                    if (c == true)
                    {
                        Session["couponcode"] = coups.couponcode.ToString();
                        Session["discount_val"] = dr["discount_val"];
                        con.Close();
                        return RedirectToAction("Carts", "CartsItems");
                    
                    }
                    else
                     {
                    con.Close();
                    return RedirectToAction("Couponfailed", "CartsItems");
                    }
                    


            return View(coups);
                
        }
        public ActionResult Couponfailed()
        {
            return View();
        }
        public ActionResult Cpayment(int id)
        {

            List<Tbl_Cart_Items> data = new List<Tbl_Cart_Items>();
            int a = Convert.ToInt32(Session["user_id"]);
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Cart_Items where user_id ='" + a + "'"))
                {
                    cmd.Connection = cn;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                ////var avg = data.Sum(x => x.product_price);
                ////ViewBag.Sumall = avg;
                //CouponClass co = new CouponClass();
                //decimal ab = 0.5m;
                //co.discount_val = ab;
                //var dis = co.discount_val;
                dt.Compute("Sum(total_val)", string.Empty);
                Convert.ToDecimal(dt.Compute("Sum(total_val)", string.Empty));
                ViewBag.discount = (Convert.ToDecimal(dt.Compute("Sum(total_val)", string.Empty)) - (Convert.ToDecimal(dt.Compute("Sum(total_val)", string.Empty)) * Convert.ToDecimal(Session["discount_val"])));
                return View(dt);
            }
        }
    }
}