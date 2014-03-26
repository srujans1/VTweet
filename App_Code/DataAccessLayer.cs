using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Models;

namespace DAL
{
/// <summary>
/// Summary description for DataAccessLayer
/// </summary>
    public static class DataAccessLayer
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static int AddVideo(Models.Video vid, Guid UserId)
        {
            int newId = -1;
            string queryString = "AddVideo";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier, 25);
                command.Parameters.Add("@URL", SqlDbType.Text);


                command.Parameters["@UserID"].Value = UserId;
                command.Parameters["@URL"].Value = vid.Url;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    command.CommandText = "select @@Identity";
                    command.CommandType = CommandType.Text;
                    newId= Convert.ToInt32(command.ExecuteScalar().ToString());

                }
                catch (Exception ex)
                {
                    return -1;
                }

            }
            return newId;

        }

        public static IEnumerable<Video> GetMyVideos(Guid UserId)
        {
            string queryString = "ListVideos";
            List<Video> myVideos = new List<Video>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier, 25);
               
                command.Parameters["@UserID"].Value = UserId;
                
                try
                {
                    connection.Open();
                    SqlDataReader dr= command.ExecuteReader();
                    while (dr.Read())
                    {
                        string url = UtilityFunctions.transcodeUrl(dr[2].ToString());
                        myVideos.Add(new Video() { VideoID = dr[0].ToString(),UserName=dr[1].ToString(), Url = url, TimeStamp = dr[3].ToString() });
                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return myVideos;

        }

      

        public static IEnumerable<Video> GetFriendVideos(Guid UserId)
        {
            string queryString = "ListFriendVideos";
            List<Video> myVideos = new List<Video>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier, 25);

                command.Parameters["@UserID"].Value = UserId;

                try
                {
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        string url = UtilityFunctions.transcodeUrl(dr[2].ToString());
                        myVideos.Add(new Video() { VideoID = dr[0].ToString(),UserName=dr[1].ToString(), Url = url, TimeStamp = dr[3].ToString() });
                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return myVideos;

        }

        public static Video GetVideo(string videoID)
        {
            string queryString = "select * from video where VideoID = '"+videoID+"'";
            Video myVideo = new Video();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.Text;
                try
                {
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        myVideo.VideoID = dr[0].ToString();
                        myVideo.Url = UtilityFunctions.transcodeUrl(dr[2].ToString());
                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return myVideo;

        }

        public static IEnumerable<Models.User> GetFriends(Guid userId, string queryString)
        {
            
            List< Models.User> userList= new List<Models.User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.UniqueIdentifier));
                command.Parameters["@UserID"].Value=userId;
               
                try
                {
                    connection.Open();
                    SqlDataReader dr= command.ExecuteReader();
                    while (dr.Read())
                    {
                        userList.Add(new Models.User() { UserID = dr["UserID"].ToString(), UserName = dr["UserName"].ToString() });
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return userList;

        }

        

        public static IEnumerable<Video> GetMyVideoResponses(string  vid)
        {
            string queryString = "select * from Video where VideoID in (select ResponseVideoID from VideoResponse where OriginalVideoID = '" + vid + "')";
            List<Video> myVideos = new List<Video>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.Text;
               

                try
                {
                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        string url = UtilityFunctions.transcodeUrl(dr[2].ToString());
                        myVideos.Add(new Video() { VideoID = dr[0].ToString(), Url = url, TimeStamp = dr[3].ToString() });
                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return myVideos;

        }


        public static bool AddResponseVideo(string orgvideo, string newVid)
        {
            string queryString = "AddResponse";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@OriginalVideoID", SqlDbType.Int);
                command.Parameters.Add("@ResponseVideoID", SqlDbType.Int);


                command.Parameters["@OriginalVideoID"].Value = orgvideo;
                command.Parameters["@ResponseVideoID"].Value = newVid;
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

        public static void UnFollowUser(Guid userID, Guid FriendUserID)
        {
           
            string queryString = "Unfollow";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@FriendUserID", SqlDbType.UniqueIdentifier);


                command.Parameters["@UserID"].Value = userID;
                command.Parameters["@FriendUserID"].Value = FriendUserID;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                 

                }
                catch (Exception ex)
                {
                    return;
                }

            }
         
        }

        public static void FollowUser(Guid userID, Guid FriendUserID)
        {

            string queryString = "Follow";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@FriendUserID", SqlDbType.UniqueIdentifier);


                command.Parameters["@UserID"].Value = userID;
                command.Parameters["@FriendUserID"].Value = FriendUserID;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    return;
                }

            }

        }
    }
}