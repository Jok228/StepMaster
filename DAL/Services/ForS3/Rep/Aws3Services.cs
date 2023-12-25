

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using API.Services.ForS3.Int;
using Domain.Entity.API;
using Microsoft.AspNetCore.Http;
using static Amazon.Internal.RegionEndpointProviderV2;

namespace API.Services.ForS3.Rep
{
    public class Aws3Services : IAws3Services
    {
        private readonly string _amazonAccessKeyId;
        private readonly string _amazonSecretAccessKey;
        private readonly string URL;
        private readonly string _bucketName;
        private readonly AmazonS3Config _config;
        const double timeoutDuration = 1;
        private readonly string _beginPath = "StepMaster/";

        public Aws3Services( string awsAccessKeyId, string awsSecretAccessKey, string bucketName, string url)
        {  
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

        public async Task<BaseResponse<string>> GetLink(string path)
        {
            var response = new BaseResponse<string>();           
            var fullPathFile = string.Empty;
          
            try
            {
                using (var _client = new AmazonS3Client(_amazonAccessKeyId, _amazonSecretAccessKey, _config))
                {
                    
                    var request = new ListObjectsV2Request()
                    {
                        BucketName = _bucketName,
                        Prefix = _beginPath + path

                    };
                    var list = await _client.ListObjectsV2Async(request);
                    foreach (var obj in list.S3Objects)
                    {
                        var name = obj.Key
                            .Split('/')[obj.Key.Split('/').Length - 1]
                            .Split('.')[0];                       
                        if(name == path.Split("/")[path.Split('/').Length - 1])
                        {
                            fullPathFile = obj.Key;
                            break;
                        }

                    }       
                    if(fullPathFile != string.Empty)
                    {
                        response.Data = GeneratePresignedURL(_client, _bucketName, fullPathFile, timeoutDuration);
                        response.Status = MyStatus.Success;
                        return response;
                    }
                    response.Data = null;
                    response.Status = MyStatus.NotFound;
                    return response;

                }
            }
            catch (Exception ex)
            {
                response.Status = MyStatus.Except;
                response.Description = ex.Message;
                Console.WriteLine(ex.Message,ex.StackTrace);
                return response;
            }
        }
        public static string GeneratePresignedURL(IAmazonS3 client, string bucketName, string objectKey, double duration)
        {
            string urlString = string.Empty;
            try
            {
                var request = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    Expires = DateTime.UtcNow.AddHours(duration),
                };
                urlString = client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error:'{ex.Message}'");
            }

            return urlString;
        }

        public async Task<BaseResponse<bool>> InsertFile(string path, IFormFile file)
        {
            var response = new BaseResponse<bool>();
            var fullPathFile = string.Empty;
            int BufferSize = 10 * 1024 * 1024;
            try
            {
                using (var _client = new AmazonS3Client(_amazonAccessKeyId, _amazonSecretAccessKey, _config))
                {
                    using (var newMemoryStream = new StreamContent(file.OpenReadStream(), bufferSize: BufferSize).ReadAsStreamAsync())
                    {
                        var request = new ListObjectsV2Request()
                        {
                            BucketName = _bucketName,
                            Prefix = _beginPath + path

                        };
                        var list = await _client.ListObjectsV2Async(request);
                        foreach (var obj in list.S3Objects)
                        {
                            var name = obj.Key
                                .Split('/')[obj.Key.Split('/').Length - 1]
                                .Split('.')[0];
                            if (name == path.Split("/")[path.Split('/').Length - 1])
                            {
                                fullPathFile = obj.Key;
                                break;
                            }

                        }
                        if (fullPathFile != string.Empty)
                        {
                            var deleteRequest = new DeleteObjectRequest
                            {

                                Key = fullPathFile,
                                BucketName = _bucketName,

                            };
                            await _client.DeleteObjectAsync(deleteRequest);
                        }
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream.Result,
                            Key = _beginPath + path + file.ContentType.Split('/')[1].Insert(0,"."),
                            BucketName = _bucketName,
                            ContentType = file.ContentType,

                        };

                        var transfere = new TransferUtility(_client);

                        await transfere.UploadAsync(uploadRequest);
                        
                    }
                }
                response.Data = true;
                response.Status = MyStatus.Success;
                return response;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);
                response.Data = false;                
                response.Status = MyStatus.Except;
                return response;
            }
           
            
        }
    }
}
