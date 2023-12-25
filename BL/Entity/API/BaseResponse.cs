namespace Domain.Entity.API
{
    public class BaseResponse<T>
    {
        public string Description { get; set; }

        public MyStatus Status { get; set; }

        public T Data { get; set; }

    }
    public enum MyStatus
    {
        Success = 200,
        SuccessCreate = 201,
        Except = 500,
        Exists = 409,
        NotFound = 404,
        Unauthorized = 418,


    }
}
