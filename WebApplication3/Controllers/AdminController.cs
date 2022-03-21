using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using WebApplication3.Models;
using System.Configuration;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin


        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_User"))
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
        
        public ActionResult Categories()
        {
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Categories"))
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



        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            con = new SqlConnection(str);

        }
        [HttpGet]
        public ActionResult AddCate()
        {

            return View();
        }

        //[HttpPost]
        //public ActionResult AddCate(CateClass cname)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {


        //            if (AddCate1(cname))
        //            {
        //                ViewBag.Message = "Categories added successfully";
        //            }
        //        }

        //        return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //public bool AddCate1(CateClass obj)
        //{

        //    connection();

        //    SqlCommand com = new SqlCommand("Insert Into Tbl_Categories(@category_name)Values('" + obj.category_name + "')", con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@category_name", obj.category_name);

        //    con.Open();
        //    int i = com.ExecuteNonQuery();
        //    con.Close();
        //    if (i >= 1)
        //    {

        //        return true;

        //    }
        //    else
        //    {

        //        return false;
        //    }
        //}
        //public ActionResult AddCate(CateClass data)
        //{
        //    //WebApplication3.Models.CateClass data = new Models.CateClass();
        //    data.category_name = Request["category_name"];
        //    int res = data.Insert();
        //    if (res > 0)
        //    {



        //    }
        //    else
        //    {

        //        return RedirectToAction("AddCate", "Admin");

        //    }

        //    return View(data);
        //}
        [HttpPost]
        public ActionResult AddCate(CateClass data)
        {
            if (ModelState.IsValid)
            {
                string query = "insert into Tbl_Categories (category_name) values('" + data.category_name + "')";
                SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = query;

                int res = cmd.ExecuteNonQuery();

                cn.Close();
                return RedirectToAction("Categories");
            }

            return View(data);
        }

        //[HttpGet]
        //public ActionResult EditCate(int id=0)
        //{
        //    CateClass data = new CateClass();
        //    List<CateClass> clist = new List<CateClass>();
        //    var std = clist.Where(s => s.category_id == id).FirstOrDefault();
        //    return View(std);
        //}
        //[HttpPost]
        //public ActionResult EditCate(CateClass std)
        //{
        //    List<CateClass> clist = new List<CateClass>();
        //    var catlist = clist.Where(s => s.category_id == std.category_id).FirstOrDefault();
        //    clist.Remove(catlist);
        //    clist.Add(std);

        //    return RedirectToAction("Categories");
        //}
        [HttpGet]
        public ActionResult EditCate(int id)
        {
            Catlayer obj = new Catlayer();
            List<Catlayer> clist = new List<Catlayer>();
            CateClass emp = obj.clist.Single(x=>x.category_id==id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult EditCate(CateClass categ)
        {
            if (ModelState.IsValid)
            {
                Catlayer obj = new Catlayer();
                obj.UpdateCat(categ);
                return RedirectToAction("Categories");
            }
            return View(categ);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Catlayer obj = new Catlayer();
            obj.DeleteCat(id);
            return RedirectToAction("Categories");
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }



    }

}