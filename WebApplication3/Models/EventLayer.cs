using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class EventLayer
    {
        public IEnumerable<EventClass> elist
        {
            get
            {
                string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                List<EventClass> evlist = new List<EventClass>();
                using (SqlConnection con = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Events", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        EventClass evobj = new EventClass();
                        //Initialization of Employee cbject
                        evobj.event_id = Convert.ToInt32(dr["event_id"]);
                        evobj.event_name = dr["event_name"].ToString();
                        evobj.E_StartDate = dr["E_StartDate"].ToString();
                        evobj.E_ExpireDate = dr["E_ExpireDate"].ToString();
                        evlist.Add(evobj);
                    }
                }
                return evlist;
            }
        }

        public void updateEvent(EventClass eid)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            int eventID = eid.event_id;
            string eName = eid.event_name;
            string eStart = eid.E_StartDate;
            string eEnd = eid.E_ExpireDate;

            string strUpdate = "update Tbl_Events set event_name='" + eName + "', E_StartDate = '"+eStart+"', E_ExpireDate='"+eEnd+"' where event_id=" + eventID;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strUpdate, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void deleteEvent(int id)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

            string strDelete = "delete Tbl_Events  where event_id=" + id;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}