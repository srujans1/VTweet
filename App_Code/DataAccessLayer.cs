using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace DAL
{
/// <summary>
/// Summary description for DataAccessLayer
/// </summary>
    public static class DataAccessLayer
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static bool AddVideo(Models.Video vid)
        {
            string queryString = "AddVideo";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier, 25);
                command.Parameters.Add("@URL", SqlDbType.VarChar, 50);


                command.Parameters["@UserID"].Value = vid.UserID;
                command.Parameters["@URL"].Value = vid.Url;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return true;

        }


    }
}