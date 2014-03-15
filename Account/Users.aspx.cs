using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RefreshDataSource();
        }
        
    }

    private void RefreshDataSource()
    {
        FriendsList.DataSource = DAL.DataAccessLayer.GetFriends((Guid)Membership.GetUser().ProviderUserKey, "ListFriends");
        FriendsList.DataBind();
        NonFriendsList.DataSource = DAL.DataAccessLayer.GetFriends((Guid)Membership.GetUser().ProviderUserKey, "ListNonFriends");
        NonFriendsList.DataBind();
    }
    protected void MyBtnHandler(Object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn.CommandName.Equals("CmdFollow"))
        {
            DAL.DataAccessLayer.FollowUser((Guid)Membership.GetUser().ProviderUserKey, new Guid(btn.CommandArgument));
        
        }
        else if (btn.CommandName.Equals("CmdUnFollow"))
        {
            DAL.DataAccessLayer.UnFollowUser((Guid)Membership.GetUser().ProviderUserKey, new Guid(btn.CommandArgument));

        }

        RefreshDataSource();
        
    }
}