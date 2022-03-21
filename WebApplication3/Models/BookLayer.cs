using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication3.Models
{
    public class BookLayer
    {
        
        public IEnumerable<BookClass> blist
        {
            get
            {
                string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                List<BookClass> booklist = new List<BookClass>();
                using (SqlConnection con = new SqlConnection(cn))
                {
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Products", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        BookClass bookobj = new BookClass();
                        //Initialization of Employee cbject
                        bookobj.product_id = Convert.ToInt32(dr["product_id"]);
                        bookobj.product_name = dr["product_name"].ToString();
                        bookobj.product_image = dr["product_image"].ToString();
                        bookobj.category_id = Convert.ToInt32(dr["category_id"]);
                        bookobj.product_price = Convert.ToInt32(dr["product_price"]);
                        bookobj.product_qty = Convert.ToInt32(dr["product_qty"]);

                        booklist.Add(bookobj);
                    }
                }
                return booklist;
            }
        }
        public void UpdateBook(BookClass bid)
        {
            string cn = @"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            int bookID = bid.product_id;
            string bname = bid.product_name;
            //string fpath = "~\\image\\upload\\"+bid.UploadFile.FileName;
            //bid.UploadFile.SaveAs(Server.MapPath("~")+f);
            //string bimg = bid.product_image;
            int cid = bid.category_id;
            int bprice = bid.product_price;
            int bqty = bid.product_qty;

            string strUpdate = "update Tbl_Products set product_name='" + bname + "', category_id='"+cid+"', product_price='"+bprice+"', product_qty='"+bqty+"' where product_id=" + bookID;
            using (SqlConnection con = new SqlConnection(cn))
            {
                SqlCommand cmd = new SqlCommand(strUpdate, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}