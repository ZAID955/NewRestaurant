using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace NewRestaurant.Helper
{
    public static  class Helper
    {
        public static string Connectionstring = @"Server=DESKTOP-4OSGQ10\SQLEXPRESS;Database=Restaurant;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";
        public static int ExecuteNonQuerycommand(string query, Dictionary<string,object> parames)
        {
            SqlConnection connection = new SqlConnection(Connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            if (parames != null)
            {
                foreach (KeyValuePair<string, object> kvp in parames)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
            }
            int rows = command.ExecuteNonQuery();
            connection.Close();
            return rows;
        }

        public static int ExecuteNonQueryWithStoredProcedure(string query, Dictionary<string, object> parames)
        {
            SqlConnection connection = new SqlConnection(Connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            if (parames != null)
            {
                foreach (KeyValuePair<string, object> kvp in parames)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
            }
            command.CommandType = CommandType.StoredProcedure;
            int rows = command.ExecuteNonQuery();
            connection.Close();
            return rows;
        }

        public static DataTable ExecuteQuerycommand(string query, Dictionary<string, object> parames)
        {
            SqlConnection connection = new SqlConnection(Connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            if (parames != null)
            {
                foreach (KeyValuePair<string, object> kvp in parames)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }
        public static DataTable ExecuteQuerycommandWithStoredProcedure(string query, Dictionary<string, object> parames)
        {
            SqlConnection connection = new SqlConnection(Connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            if (parames != null)
            {
                foreach (KeyValuePair<string, object> kvp in parames)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
            }
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }

    }
}
