using System.Net.Mail;
using System.Net;
using Application.Services.Post.Repositories;
using Domain.Entity.Main;
using Application.Logic.CodeGenerate;
using System.Globalization;
using Application.Repositories.Db.Interfaces_Repository;
using Microsoft.Extensions.Logging;
namespace Application.Services.Post.Services;


public class Post_Service : IPost_Service
{

    private string MainMail = "tusxapps.company@gmail.com";
    private string MainPassword = "pnlwsgisrvcqvqho";
    private string _smtp = "smtp.gmail.com";
    private ILogger<Post_Service> _logger;

    public Post_Service(ILogger<Post_Service> logger)
    {
        _logger = logger;
    }

    public async Task<Code> SendCodeUser(string email)
    {
        try
        {
            var codeStr = new Code()
            {
                code = CodeGenerate.GeneratedCode()
            };
            var subject = "Ваш код подтверждения: " + codeStr.code;
            var body = "<h2>StepMaster</h2>";


            await Send(body, subject, email);
            return codeStr;
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex.Message);
            throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
        }
        

    }


    public async Task SendPasswordOnMail(string email, string message, string host)
    {
        try
        {
            string body = $"<body style=\"width: 100%; background-color: rgb(255, 255, 255); \">\r\n" +
            $"    <div style=\"height: 100px; width: 90%; background-color: rgb(255, 255, 255);" +
            $" padding: 20px;margin-left: auto; margin-right: auto;\">\r\n " +
            $"       <div style=\"background-color: rgb(0, 119, 255); height:" +
            $" 60%; width: 100%;border-radius: 50px;font-size: 24px;text-align:" +
            $" center; padding:0.3%;  \">\r\n " +
            $"           <a href=\"{host}/view/SystemAnswer/AcceptPassword?email={email}&password={message}\"" +
            $" style=\"text-decoration:none; color: black; width: 900px;\" >\r\n" +
            $"                Edit password\r\n            </a>\r\n        </div>" +
            $"    \r\n    </div>\r\n    \r\n</body>";
            string subject = "На вашем аккаунте хотят сменить пароль, для подтверждения нажмите на кнопку.";
            await Send(body, subject, email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
        }
        
    }
    private async Task Send(string body,string subject,string email)
    {
        try
        {
            var client = new SmtpClient(_smtp)
            {
                Credentials = new NetworkCredential(MainMail, MainPassword),
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network

            };
            MailAddress from = new MailAddress(MainMail);
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);


            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            await client.SendMailAsync(mailMessage);            
        }
        catch (Exception ex) 
        {            
            _logger.LogError(ex.Message);
            throw new HttpRequestException("500 Shit happens", null, HttpStatusCode.NotFound);
        }
        
    }
}