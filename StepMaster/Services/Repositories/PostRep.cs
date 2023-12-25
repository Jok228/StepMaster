using System.Net;
using System.Net.Mail;
using StepMaster.Services.Interfaces;
namespace StepMaster.Services.Repositories;


public class PostRep:IPost_Service
{
    private string MainMail = "sosiska6742@gmail.com";
    private string MainName = "StepMaster";
    private string MainPassword = "Lionid!135";
    private static SmtpClient MyPost = new SmtpClient();

   
    public async Task<bool> SendMessageAsync(string email,string message)
    {
        try
        {
            MailAddress from = new MailAddress("sosiska6742@gmail.com", MainName);
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);


            mailMessage.Subject = "Ваш код подтверждения: " + message;
            mailMessage.Body = "<h2>StepMaster</h2>";
            mailMessage.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(MainMail, "pjkenuwossiczoyn");
            await smtp.SendMailAsync(mailMessage);
            return true;

        }
        catch {
            return false;
        }
        
    }

    public async Task<bool> SendPasswordOnMail(string email, string message,string host)
    {
        try
        {
            MailAddress from = new MailAddress("sosiska6742@gmail.com", MainName);
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);


            mailMessage.Subject = "На вашем аккаунте хотят сменить пароль, для подтверждения нажмите на кнопку.";
            mailMessage.Body = $"<body style=\"width: 100%; background-color: rgb(255, 255, 255); \">\r\n    <div style=\"height: 100px; width: 90%; background-color: rgb(255, 255, 255); padding: 20px;margin-left: auto; margin-right: auto;\">\r\n        <div style=\"background-color: rgb(0, 119, 255); height: 60%; width: 100%;border-radius: 50px;font-size: 24px;text-align: center; padding:0.3%;  \">\r\n            <a href=\"{host}/view/SystemAnswer/AcceptPassword?email={email}&password={message}\" style=\"text-decoration:none; color: black; width: 900px;\" >\r\n                Изменить пароль\r\n            </a>\r\n        </div>    \r\n    </div>\r\n    \r\n</body>";
            mailMessage.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(MainMail, "pjkenuwossiczoyn");
            await smtp.SendMailAsync(mailMessage);
            return true;

        }
        catch
        {
            return false;
        }
    }
}