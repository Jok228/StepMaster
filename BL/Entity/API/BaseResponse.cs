namespace Domain.Entity.API
{
    public class BaseResponse<T>
    {
        
        public string Description { get; set; }

        public MyStatus Status { get; set; }

        public T? Data { get; set; }

        static public BaseResponse<T> Create(T? value, MyStatus status)
        {
            switch (status)
            {
                case MyStatus.Success:
                    return new BaseResponse<T>()
                    {
                        Data = value,
                        Status = MyStatus.Success,
                        Description = "Success"
                    };
                case MyStatus.SuccessCreate:
                    return new BaseResponse<T>()
                    {
                        Data = value,
                        Status = MyStatus.SuccessCreate,
                        Description = "SuccessCreate"
                    };
                case MyStatus.Exists:
                    return new BaseResponse<T>()
                    {                        
                        Status = MyStatus.Exists,
                        Description = "Exists"
                    };
                case MyStatus.NotFound:
                    return new BaseResponse<T>()
                    {
                        Data = value,
                        Status = MyStatus.NotFound,
                        Description = "NotFound"

                    };
                case MyStatus.Except:
                    return new BaseResponse<T>()
                    {
                        Data = value,
                        Status = MyStatus.Except,
                        Description = "Except"
                    };
                case MyStatus.BadRequest:
                    return new BaseResponse<T>()
                    {
                        Data = value,
                        Status = MyStatus.BadRequest,
                        Description = "BadRequest"
                    };
                case MyStatus.Unauthorized:
                    return new BaseResponse<T>()
                    {
                        Data = value,
                        Status = MyStatus.Unauthorized,
                        Description = "Unauthorized"
                    };
            }
            return null;

           
        }

    }
    public enum MyStatus
    {
        Success = 200,
        SuccessCreate = 201,
        Except = 500,
        BadRequest = 400,
        Exists = 409,
        NotFound = 404,
        Iteapot = 418,
        Unauthorized = 403,

    }
}
