
using Domain.Entity.Main;

namespace Application.Services.Post.Repositories;

public interface IPost_Service
{
    Task<Code> SendCodeUser(string email);

    Task SendPasswordOnMail(string email, string message, string host);
}