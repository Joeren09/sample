using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class Catlayer
    {
        public IEnumerable<CateClass> clist
        {
            get
            {
                string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                List<CateClass> catlist = new List<CateClass>();
                using (SqlConnection con = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Categories", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CateClass catobj = new CateClass();
                        //Initialization of Employee cbject
                        catobj.category_id = Convert.ToInt32(dr["category_id"]);
                        catobj.category_name = dr["category_name"].ToString();
                        catlist.Add(catobj);
                    }
                }
                return catlist;
            }
        }
        public void UpdateCat(CateClass cid)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            int catID = cid.category_id;
            string catName = cid.category_name;

            string strUpdate = "update Tbl_Categories set category_name='" + catName + "' where category_id=" + catID;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strUpdate, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteCat(int id)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

            string strDelete = "delete Tbl_Categories  where category_id=" + id;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}