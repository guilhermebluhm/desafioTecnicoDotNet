using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Projeto02.Models
{

    public class DAL
    {

        private SqlConnection Con;
        private SqlCommand cmd;
        private DataTable dt;
        private SqlDataAdapter sda;
        private string ConnString;

        public DAL() {
            ConnString = "Data Source=DESKTOP-VUHU0O8;Initial Catalog=exameTecnico;Integrated Security=True";
            Con = new SqlConnection(ConnString);
            cmd = new SqlCommand();
            cmd.Connection = Con;
        }

        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            sda = new SqlDataAdapter(Query, ConnString);
            sda.Fill(dt);
            return dt;
        }

        public int setData(String query)
        {
            int rowCount = 0;
            if(Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }

            cmd.CommandText = query;
            rowCount = cmd.ExecuteNonQuery();
            return rowCount;
        }

        public string getSingleData(String query)
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                cmd.CommandText = query;
                return (string) cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}