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
    public class EventsTableController : Controller
    {
        // GET: EventsTable
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            string str = "data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            using (SqlConnection cn = new SqlConnection(str))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Events"))
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
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(EventClass data)
        {
            if (ModelState.IsValid)
            {
                string query = "insert into Tbl_Events (event_name,E_StartDate,E_ExpireDate) values('" + data.event_name + "', '" + data.E_StartDate + "','" + data.E_ExpireDate + "')";
                SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = query;

                int res = cmd.ExecuteNonQuery();

                cn.Close();


                return RedirectToAction("Index");
            }
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EventLayer obj = new EventLayer();
            List<EventLayer> elist = new List<EventLayer>();
            EventClass data = obj.elist.Single(x=>x.event_id==id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(EventClass evclass)
        {
            if (ModelState.IsValid)
            {
                EventLayer obj = new EventLayer();
                obj.updateEvent(evclass);
                return RedirectToAction("Index");
            }
            return View(evclass);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            EventLayer obj = new EventLayer();
            obj.deleteEvent(id);
            return RedirectToAction("Index");
        }
    }
}