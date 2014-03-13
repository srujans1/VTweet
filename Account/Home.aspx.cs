using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Web.Security;

public partial class Account_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Repeater1.DataSource = DAL.DataAccessLayer.GetFriendVideos((Guid)Membership.GetUser().ProviderUserKey);
            Repeater1.DataBind();
        
        }
    }

    protected void SentBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile.aspx");
    }

    protected void UploadBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploadvideo.aspx");
    }

    protected void MyBtnHandler(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Response.Redirect("Reply.aspx?vid=" + btn.CommandArgument.ToString());
    }
}