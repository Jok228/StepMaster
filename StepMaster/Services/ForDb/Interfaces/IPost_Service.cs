namespace StepMaster.Services.ForDb.Interfaces;

public interface IPost_Service
{
    Task<bool> SendMessageAsync(string email, string message);

    Task<bool> SendPasswordOnMail(string email, string message, string host);
}