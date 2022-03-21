using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class CouponLayer
    {
        private static Random random = new Random();

        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void updatecoupon(CouponClass cid)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            int couponID = cid.coupon_id;
            string cname = cid.coupon_name;
            int eid = cid.event_id;
            decimal disval = cid.discount_val;

            string strUpdate = "update Tbl_Coupons set coupon_name='" + cname + "', event_id = '" + eid + "', discount_val='" + disval + "' where coupon_id=" + couponID;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strUpdate, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        ///////get list
        public IEnumerable<CouponClass> colist
        {
            get
            {
                string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                List<CouponClass> colist = new List<CouponClass>();
                using (SqlConnection con = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Coupons", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CouponClass cobj = new CouponClass();
                        //Initialization of Employee cbject
                        cobj.coupon_id = Convert.ToInt32(dr["coupon_id"]);
                        cobj.coupon_name = dr["coupon_name"].ToString();
                        cobj.event_id = Convert.ToInt32(dr["event_id"]);
                        cobj.discount_val = Convert.ToDecimal(dr["discount_val"]);

                        colist.Add(cobj);
                    }
                }
                return colist;
            }
        }

        public void deleteCoupon(int id)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

            string strDelete = "delete Tbl_Coupons  where coupn_id=" + id;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}