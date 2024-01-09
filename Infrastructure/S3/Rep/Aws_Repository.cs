

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using API.Services.ForS3.Configure;
using Application.Repositories.S3.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services.ForS3.Rep
{
    public class Aws_Repository : IAws_Repository
    {
        private readonly IAppConfiguration _configure;
        private readonly string _amazonAccessKeyId;
        private readonly string _amazonSecretAccessKey;
        
        private readonly int _bufferSize = 10 * 1024 * 1024;
        private readonly string _bucketName;
        private readonly AmazonS3Config _config;
        const double timeoutDuration = 5;
        private readonly string _beginPath = "StepMaster/";
        private string _pathUserAwatar = "/Icons/Avatar";

        public Aws_Repository(IAppConfiguration configure)
        {  
            _configure = configure;
            _amazonAccessKeyId = configure.AwsAccessKey;
            _amazonSecretAccessKey = configure.AwsSecretAccessKey;
            _bucketName = configure.BucketName;
            _config = new AmazonS3Config()
            {
                ServiceURL = configure.URL
            };  
        }

        public async Task<string> GetUserAvatarLink(string userName)
        {
            var path = userName + _pathUserAwatar;
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
                        var response = await GetLink( fullPathFile);
                        return response;
                    }                    
                    return null;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message,ex.StackTrace);
                return null;
            }
        }
        public async Task<string> GetLink(string path)
        {

            string urlString = string.Empty;
            try
            {
                using (var _client = new AmazonS3Client(_amazonAccessKeyId, _amazonSecretAccessKey, _config))
                {
                    var request = new GetPreSignedUrlRequest()
                    {
                        BucketName = _bucketName,
                        Key = path,
                        Expires = DateTime.UtcNow.AddHours(timeoutDuration),
                    };
                    urlString = await _client.GetPreSignedURLAsync(request);
                }
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error:'{ex.Message}'");
            }

            return urlString;
        }

        public async Task<bool> InsertFile(string userName, IFormFile file)
        {
            var path = userName + _pathUserAwatar;            
            var fullPathFile = string.Empty;
            try
            {
                using (var _client = new AmazonS3Client(_amazonAccessKeyId, _amazonSecretAccessKey, _config))
                {
                    using (var newMemoryStream = new StreamContent(file.OpenReadStream(), bufferSize: _bufferSize).ReadAsStreamAsync())
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
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message + " - - - " + ex.StackTrace);               
                return false;
            }
           
            
        }

        public async Task<ListObjectsResponse> GetListFiles(string path)
        {
            try
            {
                using (var _client = new AmazonS3Client(_amazonAccessKeyId, _amazonSecretAccessKey, _config))
                {
                    var files = await _client.ListObjectsAsync(_bucketName, path);
                    files.S3Objects.Remove(files.S3Objects.Find(f => f.Key == path));
                    return files;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ListObjectsResponse();
            }
        }

        public async Task<IEnumerable<string>> GetFolders(string path)
        {
            try
            {
                var list = await GetListFiles(path);
                var folders = list.S3Objects.Select(x => x.Key).Where(x => x.EndsWith(@"/")).ToList();
                var mainPath = folders.Find(x => x == path+'/');
                folders.Remove(mainPath);
                return folders;
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }
    }
}
