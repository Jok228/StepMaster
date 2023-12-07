using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;


using API.Services.ForS3.Int;

using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SharpCompress.Common;

using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace API.Services.ForS3.Rep
{
    public class Aws3Services : IAws3Services
    {
        private readonly string _amazonAccessKeyId;
        private readonly string _amazonSecretAccessKey;
        private readonly string URL;
        private readonly string _bucketName;
        private readonly AmazonS3Config _config;
        

        public Aws3Services( string awsAccessKeyId, string awsSecretAccessKey, string bucketName, string url)
        {
            
            var newRegion = RegionEndpoint.GetBySystemName("ru-1");
            _amazonAccessKeyId = awsAccessKeyId;
            _amazonSecretAccessKey = awsSecretAccessKey;
            URL = url;

            AmazonS3Config config = new AmazonS3Config()
            {
                ServiceURL = URL,
            };

            _bucketName = bucketName;
            _config = config;
        }

        public async Task<GetObjectResponse> GetIcon()
        {
            try
            {
                using (var _client = new AmazonS3Client(_amazonAccessKeyId, _amazonSecretAccessKey, _config))
                {
                    GetObjectRequest getObjectRequest = new GetObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = "video/15ss.mp4"
                    };

                    var response = await _client.GetObjectAsync(getObjectRequest);

                    return response;
                }
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
    }
}
