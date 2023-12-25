using Domain.Entity.API;

namespace StepMaster.Auth.ResponseLogic
{
    public class ResponseLogic<T>
    {
        public static T Response(HttpResponse response, MyStatus status, T? answerData)
        {
            switch (status)
            {
                case MyStatus.Success:
                    response.StatusCode = (int)status;
                    return answerData;
                case MyStatus.SuccessCreate:
                    response.StatusCode = (int)status;
                    return answerData;
                case MyStatus.Exists:
                    response.StatusCode = (int)status;
                    return answerData;
                case MyStatus.Unauthorized:
                    response.StatusCode = (int)status;
                    return answerData;
                case MyStatus.BadRequest:
                    response.StatusCode= (int)status;
                    return answerData;
                case MyStatus.Except:
                    response.StatusCode= (int)status;
                    return answerData;
                case MyStatus.NotFound:
                    response.StatusCode = (int)status;
                    return answerData;
                case MyStatus.Iteapot:
                    response.StatusCode= (int)status;
                    return answerData;

            }
            return answerData;
        }
    }
}
