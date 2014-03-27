using System;
using System.IO;
using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;

using Amazon.IdentityManagement;
using Amazon.IdentityManagement.Model;

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;


/// <summary>
/// Summary description for TranscoderUtility
/// </summary>
 public class TranscoderUtility
    {
        // Policy assigned to the IAM role the pipeline is configured for.
        const string IAM_ROLE_POLICY =
            "{" +
            "    \"Version\" : \"2008-10-17\"," +
            "    \"Statement\" : [{" +
            "            \"Sid\" : \"1\"," +
            "            \"Effect\" : \"Allow\"," +
            "            \"Action\" : [\"s3:ListBucket\", \"s3:Put*\", \"s3:Get*\", \"s3:*MultipartUpload*\"]," +
            "            \"Resource\" : \"*\"" +
            "        }, {" +
            "            \"Sid\" : \"2\"," +
            "            \"Effect\" : \"Allow\"," +
            "            \"Action\" : \"sns:Publish\"," +
            "            \"Resource\" : \"*\"" +
            "        }, {" +
            "            \"Sid\" : \"3\"," +
            "            \"Effect\" : \"Deny\"," +
            "            \"Action\" : [\"s3:*Policy*\", \"sns:*Permission*\", \"s3:*Acl*\", \"sns:*Delete*\", \"s3:*Delete*\", \"sns:*Remove*\"]," +
            "            \"Resource\" : \"*\"" +
            "        }" +
            "    ]" +
            "}";

        // This field is added to the resources created to make sure their names are unique.
        static readonly string UNIQUE_POSTFIX = "-" + DateTime.Now.Ticks;

        public static void Transcode(string inputS3Key,string outputS3Key, string bucketName)
        {
           
            var email = "srujans143@gmail.com";

            // Create a topic the the pipeline to used for notifications
            var topicArn = CreateTopic(email);
          
            // Create the IAM role for the pipeline
            var role = CreateIamRole();
       


            var etsClient = new AmazonElasticTranscoderClient();

            var notifications = new Notifications()
            {
                Completed = topicArn,
                Error = topicArn,
                Progressing = topicArn,
                Warning = topicArn
            };

            // Create the Elastic Transcoder pipeline for transcoding jobs to be submitted to.
            //var pipeline = etsClient.CreatePipeline(new CreatePipelineRequest
            //{
            //    Name = "MyVideos" + UNIQUE_POSTFIX,
            //    InputBucket = bucketName,
            //    OutputBucket = bucketName,
            //    Notifications = notifications,
            //    Role = role.Arn
            //}).Pipeline;
            var listPipeLines = etsClient.ListPipelines();
            var pipeline=listPipeLines.Pipelines[0];
           
            // Create a job to transcode the input file
            etsClient.CreateJob(new CreateJobRequest
            {
                PipelineId = pipeline.Id,
                Input = new JobInput
                {
                    AspectRatio = "auto",
                    Container = "auto",
                    FrameRate = "auto",
                    Interlaced = "auto",
                    Resolution = "auto",
                    Key = inputS3Key
                },
                Output =( new CreateJobOutput
                {
                    ThumbnailPattern = "",
                    Rotate = "0",
                    // Generic 720p: Go to http://docs.aws.amazon.com/elastictranscoder/latest/developerguide/create-job.html#PresetId to see a list of some
                    // of the support presets or call the ListPresets operation to get the full list of available presets
                    PresetId = "1351620000000-100010",
                    Key = outputS3Key.Substring(0,outputS3Key.LastIndexOf("."))+".mp4"
                    
      

                })
            });

          
        }


        /// <summary>
        /// Utility method for creating at topic and subscribe the email address to it.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        static string CreateTopic(string emailAddress)
        {
            var snsClient = new AmazonSimpleNotificationServiceClient();
            var topicArn = snsClient.CreateTopic(new CreateTopicRequest
            {
                Name = "TranscodeEvents" + UNIQUE_POSTFIX
            }).TopicArn;

            snsClient.Subscribe(new SubscribeRequest
            {
                TopicArn = topicArn,
                Protocol = "email",
                Endpoint = emailAddress
            });

            return topicArn;
        }

        /// <summary>
        /// Create the IAM role that is used by the pipeline
        /// </summary>
        /// <returns></returns>
        static Role CreateIamRole()
        {
            var iamClient = new AmazonIdentityManagementServiceClient();
            var role = iamClient.GetRole(new GetRoleRequest
            {
                RoleName = "TranscodeRole-635313859828984666",
             }).Role;

            return role;
        }

        
    }




/*******************************************************************************
* Copyright 2009-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
* 
* Licensed under the Apache License, Version 2.0 (the "License"). You may
* not use this file except in compliance with the License. A copy of the
* License is located at
* 
* http://aws.amazon.com/apache2.0/
* 
* or in the "license" file accompanying this file. This file is
* distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied. See the License for the specific
* language governing permissions and limitations under the License.
*******************************************************************************/


