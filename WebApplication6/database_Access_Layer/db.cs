using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication6.database_Access_Layer
{
    public class db
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cons"].ConnectionString);

        public int Admin_Login(string adminid, string password)
        {
            int res = 0;
            SqlCommand com = new SqlCommand("Sp_Login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Admin_id", adminid);
            com.Parameters.AddWithValue("@Password", password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "@Isvalid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);
            con.Open();
            com.ExecuteNonQuery();
            res = Convert.ToInt32(oblogin.Value);
            return res;
        }
    }
}