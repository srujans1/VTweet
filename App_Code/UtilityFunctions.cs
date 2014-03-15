using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for UtilityFunctions
/// </summary>
public static class UtilityFunctions
{
    public static bool S3FolderExists(string bucketName,string folderName)
    {
        IAmazonS3 m_S3Client;
        using (m_S3Client = Amazon.AWSClientFactory.CreateAmazonS3Client(System.Web.Configuration.WebConfigurationManager.AppSettings[0].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings[1].ToString()))
        {
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = bucketName;
            request.Prefix = folderName + "/";
            request.MaxKeys = 1;
            ListObjectsResponse response = m_S3Client.ListObjects(request);
            return (response.S3Objects.Count > 0);
        }
    }

    public static void CreateFolder(string bucketName, string folderName)
    {
        IAmazonS3 m_S3Client;
        using (m_S3Client = Amazon.AWSClientFactory.CreateAmazonS3Client(System.Web.Configuration.WebConfigurationManager.AppSettings[0].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings[1].ToString()))
        {
            var folderKey = folderName + "/"; //end the folder name with "/"

            var request = new PutObjectRequest();

            request.BucketName = bucketName;

            request.StorageClass = S3StorageClass.Standard;
            request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.None;

            //request.CannedACL = S3CannedACL.BucketOwnerFullControl;

            request.Key = folderKey;

            request.ContentBody = string.Empty;

            PutObjectResponse response = m_S3Client.PutObject(request);
        }
    }
    public static string GenerateChar()
    {
        StringBuilder randomString = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i <4; i++)
        {
            randomString.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString());
        }

        return randomString.ToString();
    }
}