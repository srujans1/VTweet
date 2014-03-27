using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Reply : System.Web.UI.Page
{
    private string cloudFrontUrl = "http://d480k7tdn2p6y.cloudfront.net/";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            RefreshPage();

        
        }

    }

    private void RefreshPage()
    {
        if (Request.QueryString["vid"] != null)
        {
            string vid = Request.QueryString["vid"];
            vid1.Src = DAL.DataAccessLayer.GetVideo(vid).Url;
            Responser.DataSource = DAL.DataAccessLayer.GetMyVideoResponses(vid);
            Responser.DataBind();
        }
    }

    protected void UploadFileButton_Click(object sender, EventArgs e)
    {

        string filePath = Server.MapPath(VideoUploader.PostedFile.FileName);
        string existingBucketName = "ccdem";
        string keyName = Membership.GetUser().ProviderUserKey.GetHashCode().ToString(); 
        string fileName = UtilityFunctions.GenerateChar() + VideoUploader.PostedFile.FileName;
        IAmazonS3 client;
        using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(System.Web.Configuration.WebConfigurationManager.AppSettings[0].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings[1].ToString()))
        {

            var stream = VideoUploader.FileContent;

            stream.Position = 0;

            PutObjectRequest request = new PutObjectRequest();
            request.InputStream = stream;
            request.BucketName = existingBucketName;
            request.CannedACL = S3CannedACL.PublicRead;
            request.Key = keyName + "/" + fileName;
            PutObjectResponse response = client.PutObject(request);
        }

        string bucketUrl = "https://s3-us-west-2.amazonaws.com/" + existingBucketName + "/" + keyName + "/" + fileName;
        cloudFrontUrl = cloudFrontUrl + keyName + "/" + fileName;
        TranscoderUtility.Transcode(keyName + "/" + fileName, keyName + "/mob_" + fileName, existingBucketName);
       // lblPath.Text = "<br/>Successfully uploaded into S3:" + bucketUrl + "<br/> Cloudfront distribution url is " + cloudFrontUrl;
        Models.Video video = new Models.Video() { Url = cloudFrontUrl };
        int newVid = DAL.DataAccessLayer.AddVideo(video, (Guid)Membership.GetUser().ProviderUserKey);
        string vid=Request.QueryString["vid"].ToString();
        if(!vid.Equals(""))
            DAL.DataAccessLayer.AddResponseVideo(vid,newVid.ToString());
        RefreshPage();

        
    }
}