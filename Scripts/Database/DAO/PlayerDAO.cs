using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DbConnection
    {
        private SqlDataAdapter dataAdapter;
        private SqlConnection connection;

        public DbConnection(string connectionString)
        {
            try
            {
                dataAdapter = new SqlDataAdapter();
                connection = new SqlConnection(connectionString);
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.StackTrace);
            }

        }

        private SqlConnection OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Broken ||
                        connection.State == ConnectionState.Closed)
                    connection.Open();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Khong the ket noi toi CSDL!" + e.StackTrace);
                Application.Exit();
            }


            return connection;
        }

        public DataTable ExecuteSelectQuery(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = OpenConnection();
                cmd.CommandText = query;
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Error - Connection.executeUpdateQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            return dataTable;
        }

        public bool ExecuteInsertQuery(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = OpenConnection();
                cmd.CommandText = query;
                cmd.Parameters.AddRange(parameters);
                dataAdapter.InsertCommand = cmd;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error - Connection.executeInsertQuery - Query:" + query + " \nException: \n" + e.StackTrace.ToString());
                return false;
            }
            return true;
        }

        public bool ExecuteUpdateQuery(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = OpenConnection();
                cmd.CommandText = query;
                cmd.Parameters.AddRange(parameters);
                dataAdapter.UpdateCommand = cmd;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeUpdateQuery - Query: " + query + " \nException: " + e.StackTrace.ToString());
                return false;
            }
            return true;
        }
    }
}
