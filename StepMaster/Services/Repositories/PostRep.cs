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
}