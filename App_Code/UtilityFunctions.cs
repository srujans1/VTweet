using Amazon;
using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for UtilityFunctions
/// </summary>
public static class UtilityFunctions
{
    static Regex MobileCheck = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
    static Regex MobileVersionCheck = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);


    private static bool CheckUrlStatus(string filename)
    {
     
//       return S3FolderExists("ccdem",Membership.GetUser().ProviderUserKey.GetHashCode().ToString()+"/"+filename);

        IAmazonS3 m_S3Client;
        using (m_S3Client = Amazon.AWSClientFactory.CreateAmazonS3Client(System.Web.Configuration.WebConfigurationManager.AppSettings[0].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings[1].ToString()))
        {
            var request = new GetObjectRequest { BucketName = "ccdem", Key = Membership.GetUser().ProviderUserKey.GetHashCode().ToString() + "/" + filename };

            try
            {
                var response = m_S3Client.GetObject(request);
                if (response.ResponseStream != null)
                {
                    return true;
                }
            }
            catch (AmazonS3Exception)
            {
                return false;
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
    public static string transcodeUrl(string p)
    {
        if (UtilityFunctions.fBrowserIsMobile())
        {
            int index = p.LastIndexOf("/");
            string filename = p.Substring(index + 1);
            filename = filename.Substring(0, filename.LastIndexOf("."));
            string newFileName= p.Substring(0, index) + "/mob_" + filename+".mp4";
           // return CheckUrlStatus("mob_" + filename) ? newFileName : p;
            return newFileName;
        }
        return p;
    }
    public static bool fBrowserIsMobile()
    {
        //Debug.Assert(HttpContext.Current != null);

        if (HttpContext.Current.Request != null && HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            var u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();

            if (u.Length < 4)
                return false;

            if (MobileCheck.IsMatch(u) || MobileVersionCheck.IsMatch(u.Substring(0, 4)))
                return true;
        }

        return false;
    }

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