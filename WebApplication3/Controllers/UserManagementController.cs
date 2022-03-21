using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AccLayer obj = new AccLayer();
            List<AccLayer> acclist = new List<AccLayer>();
            AccClass emp = obj.alist.Single(x => x.user_id == id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(AccClass accobj)
        {
            if (ModelState.IsValid)
            {
                AccLayer obj = new AccLayer();
                obj.UpdateAcc(accobj);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AccLayer obj = new AccLayer();
            obj.DeleteAcc(id);
            return RedirectToAction("Index");
        }
    }
}