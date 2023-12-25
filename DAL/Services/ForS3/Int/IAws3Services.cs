using Amazon.S3.Model;
using Domain.Entity.API;
using Microsoft.AspNetCore.Http;



namespace API.Services.ForS3.Int
{
    public interface IAws3Services
    {

       public Task<BaseResponse<string>> GetLink(string path);

       public Task<BaseResponse<bool>> InsertFile(string path, IFormFile file);


    }
}
