using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
    }

    public class Video
    {
        public string UserName { get; set; }
        public string VideoID { get; set; }
        public string Url { get; set; }
        public string Mob_Url { get; set; }
        public string TimeStamp { get; set; }
    }

    public class Friend
    {
        public string FriendUserID { get; set; }
        public string FriendUserName { get; set; }
    }
}
