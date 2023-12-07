using Amazon.S3.Model;


namespace API.Services.ForS3.Int
{
    public interface IAws3Services
    {

       public Task<GetObjectResponse> GetIcon();


    }
}
