using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication3.Models
{
    public class AccLayer
    {
        public IEnumerable<AccClass> alist
        {
            get
            {
                string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                List<AccClass> userlist = new List<AccClass>();
                using (SqlConnection con = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("select * from Tbl_User", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        AccClass Accobj = new AccClass();
                        //Initialization of Employee cbject
                        Accobj.user_id = Convert.ToInt32(dr["user_id"]);
                        Accobj.email = dr["email"].ToString();
                        Accobj.contact_no = dr["contact_no"].ToString();
                        userlist.Add(Accobj);
                    }
                }
                return userlist;
            }
        }
        public void UpdateAcc(AccClass uid)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            int userid = uid.user_id;
            string username = uid.email;
            string upass = uid.password;
            string contactn = uid.contact_no;


            string strUpdate = "update Tbl_User set email='" + username + "', password ='"+upass+"', contact_no='"+contactn+"' where user_id=" + userid;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strUpdate, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAcc(int id)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

            string strDelete = "delete Tbl_User  where user_id=" + id;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}