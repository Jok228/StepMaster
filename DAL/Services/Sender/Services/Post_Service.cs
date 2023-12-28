using System.Net.Mail;
using System.Net;
using Application.Services.Post.Repositories;
using Domain.Entity.Main;
using Domain.Entity.API;
using Application.Logic.CodeGenerate;
using System.Globalization;
namespace Application.Services.Post.Services;


public class Post_Service : IPost_Service
{

    private string MainMail = "tusxapps.company@gmail.com";
    private string MainPassword = "pnlwsgisrvcqvqho";
    private string _smtp = "smtp.gmail.com";
    public async Task<BaseResponse<Code>> SendCodeUser(string email)
    {

        var codeStr = new Code()
        {
            code = CodeGenerate.GeneratedCode()
        };
        var subject = "Ваш код подтверждения: " + codeStr.code;
        var body = "<h2>StepMaster</h2>";
        
        return await Send(body, subject, email)? BaseResponse<Code>.Create(codeStr, MyStatus.Success) : BaseResponse<Code>.Create(codeStr, MyStatus.Except);

    }


    public async Task<BaseResponse<bool>> SendPasswordOnMail(string email, string message, string host)
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
        return await Send(body, subject, email)?BaseResponse<bool>.Create(true,MyStatus.Success):BaseResponse<bool>.Create(false,MyStatus.Except);
    }
    private async Task<bool> Send(string body,string subject,string email)
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
            return true;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        
    }
}