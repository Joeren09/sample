using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class CateClass
    {
        public int category_id { get; set; }

        [Required]
        public string category_name { get; set; }

        //public List<CateClass> CatInfo { get; set; }

        //public int Insert(string catname)
        //{
        //    SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        //    SqlCommand cmd = new SqlCommand("Insert Into Tbl_Categories(category_name)Values('" + catname + "')", cn);
        //    cn.Open();
        //    return cmd.ExecuteNonQuery();
        //}
        //public int Insert(CateClass data)
        //{
        //    string query = "insert into Tbl_Categories(category_name) values('" + data.category_name + "')";
        //    SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        //    cn.Open();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = cn;
        //    cmd.CommandText = query;
        //    int res = cmd.ExecuteNonQuery();
        //    cn.Close();
        //    return res;

        //}

        //insert method which using the current object properties
    //    public int Insert()
    //{
    //    string query = "insert into Tbl_Categories (category_name) values('" + category_name + "')";
    //    SqlConnection cn = new SqlConnection(@"data source=DESKTOP-CPP9G02;initial catalog=dbReadNLearn;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
    //    cn.Open();
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = cn;
    //    cmd.CommandText = query;
    //    int res = cmd.ExecuteNonQuery();
    //    cmd.Dispose();
    //    cn.Close();
    //    return res;

    //}

}
}