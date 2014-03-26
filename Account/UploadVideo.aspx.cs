using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using Amazon.EC2;
using Amazon.S3.Model;

using System.Web.Security;


public partial class Account_UploadVideo : System.Web.UI.Page
{
    private string cloudFrontUrl="http://d480k7tdn2p6y.cloudfront.net/";
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }
    protected void UploadFileButton_Click(object sender, EventArgs e)
    {

        string filePath = Server.MapPath(VideoUploader.PostedFile.FileName);
        string existingBucketName = "ccdem";
        string keyName = Membership.GetUser().ProviderUserKey.GetHashCode().ToString();
        string fileName = UtilityFunctions.GenerateChar()+VideoUploader.PostedFile.FileName;
        IAmazonS3 client;
        using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(System.Web.Configuration.WebConfigurationManager.AppSettings[0].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings[1].ToString()))
        {

            var stream = VideoUploader.FileContent;

            stream.Position = 0;

            PutObjectRequest request = new PutObjectRequest();
            request.InputStream = stream;
            request.BucketName = existingBucketName;
            request.CannedACL = S3CannedACL.PublicRead;
            //if (!UtilityFunctions.S3FolderExists(existingBucketName, keyName))
            //{
            //    UtilityFunctions.CreateFolder(existingBucketName, keyName);
            //}
            request.Key = keyName + "/" + fileName;
            PutObjectResponse response = client.PutObject(request);
        }

        string bucketUrl = "https://s3-us-west-2.amazonaws.com/" + existingBucketName + "/" + keyName + "/" + fileName;
        cloudFrontUrl =  cloudFrontUrl+ keyName + "/" + fileName;
        TranscoderUtility.Transcode(keyName + "/" + fileName, keyName + "/mob_" + fileName, existingBucketName);
        //lblPath.Text = "<br/>Successfully uploaded into S3:"+bucketUrl + "<br/> Cloudfront distribution url is "+cloudFrontUrl;
        Models.Video video = new Models.Video() { Url = cloudFrontUrl };
        DAL.DataAccessLayer.AddVideo(video, (Guid)Membership.GetUser().ProviderUserKey);
    }
}