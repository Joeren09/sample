using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApplication3.Models;
using System.Web.Mvc;
using System.IO;

namespace WebApplication3.Controllers
{
    public class ProductsController : Controller
    {
        
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Carts()
        {
            return View();
        }
        public ActionResult Coupons()
        {
            return View();
        }
        public ActionResult ABookList()
        {
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Products"))
                {
                    cmd.Connection = cn;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return View(dt);
        }
        [HttpGet]
        public ActionResult EditBook(int id)
        {
            BookLayer obj = new BookLayer();
            List<BookLayer> blist = new List<BookLayer>();
            BookClass emp = obj.blist.Single(x => x.product_id == id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult EditBook(BookClass bk, HttpPostedFileBase UploadFile)
        {
            try
            {
                //string Iname = Path.GetFileName(bk.UploadFile.FileName);
                //string fpath = "\\UploadFile\\" + bk.UploadFile.FileName;
                //string fpath = "\\UploadFile\\" + Iname;
                //bk.UploadFile.SaveAs(Server.MapPath("~") + fpath);
                //bk.product_image = fpath;
                //savedb(bk);
                if (UploadFile != null && UploadFile.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/UploadFile/"), Path.GetFileName(UploadFile.FileName));
                    UploadFile.SaveAs(filePath);
                }

                if (ModelState.IsValid)
            {
                BookLayer obj = new BookLayer();
                obj.UpdateBook(bk);

                
                return RedirectToAction("ABookList");
            }
 
            return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
                return View();
            }
        }

        public string savedb(BookClass bmg)
        {
            try
            {
                string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                SqlConnection con = new SqlConnection(str);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "UpImg";
                cmd.Parameters.AddWithValue("@Image", bmg.product_image);
                cmd.Parameters.AddWithValue("@p_id", bmg.product_id);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                con.Dispose();
                con.Close();

                return "Saved Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        

    }
}