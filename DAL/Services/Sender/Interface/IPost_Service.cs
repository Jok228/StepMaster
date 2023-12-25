using Domain.Entity.API;
using Domain.Entity.Main;

namespace Application.Services.Post.Repositories;

public interface IPost_Service
{
    Task<BaseResponse<Code>> SendCodeUser(string email);

    Task<BaseResponse<bool>> SendPasswordOnMail(string email, string message, string host);
}