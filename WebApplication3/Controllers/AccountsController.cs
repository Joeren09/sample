using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Data;
using System.Net;
using System.Data.Entity;
using System.Data.SqlClient;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class AccountsController : Controller
    {
      
        // GET: Accounts
        public ActionResult Index()
        {
            List<AccClass> accClasses = new List<AccClass>();
            return View("Index",accClasses);
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Payment()
        {
            return View();
        }
       
        public ActionResult UserProfile()
        {
            //int a = (int)Session["user_id"];
            int a = Convert.ToInt32(Session["user_id"]);
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_User where user_id ='" + a + "'"))
                {
                    cmd.Connection = cn;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                return View(dt);
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            
        }

        [HttpPost]
        public ActionResult Verify(Login acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from Tbl_User where email = '" + acc.email + "' and password = '" + acc.password + "'; select * from Tbl_Admin where email = '" + acc.email + "' and password = '" + acc.password + "'; select user_id from Tbl_User where email = '" + acc.email + "' and password = '" + acc.password + "' ";
            dr = com.ExecuteReader();
            string umail = acc.email;
            bool c = dr.Read();
            dr.NextResult();
            bool a = dr.Read();

            ///////////////////////////////////////
            dr.NextResult();
            bool uid = dr.Read();

            //////////////////////////////////////

            if (c == true && uid == true)
            {
                Session["email"] = acc.email.ToString();
                Session["user_id"] = dr["user_id"];
                con.Close();
                return RedirectToAction("Index","Home");
                
            }
            else if (a == true)
            {
                Session["email"] = acc.email.ToString();
                con.Close();
                return RedirectToAction("Index","Admin");
            }
            else if (a !=true || c!= true)
            {
                con.Close();
                return RedirectToAction("LoginFail","Accounts");
            }
            return View(acc);

        }

        //~/Views/Admin/AdminIndex.cshtml

        public ActionResult LoginFail()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Signup(AccClass data)
        {
            if (ModelState.IsValid)
            {
                //var shortDT = defaultDate.ToString().Replace("12:00:00 AM", "");
                string udate = DateTime.Now.ToString("MM/dd/yyyy");
                // string cnumber = data.contact_no.ToString();
                string query = "insert into Tbl_User (email,password,contact_no,date_of_reg) values('" + data.email + "', '" + data.password + "','" + data.contact_no + "','" + udate + "')";
                SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = query;

                int res = cmd.ExecuteNonQuery();

                cn.Close();
                ViewBag.Message = "Account Created";
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            AccLayer obj = new AccLayer();
            List<AccLayer> acclist = new List<AccLayer>();
            AccClass emp = obj.alist.Single(x=>x.user_id==id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult UpdateUser(AccClass accobj)
        {
            if (ModelState.IsValid)
            {
                AccLayer obj = new AccLayer();
                obj.UpdateAcc(accobj);
                return RedirectToAction("UserProfile");
            }
            return View();
        }
    }
}