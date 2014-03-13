using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            videoRepeater.DataSource = DAL.DataAccessLayer.GetMyVideos((Guid)Membership.GetUser().ProviderUserKey);
            videoRepeater.DataBind();

        }
    }
}