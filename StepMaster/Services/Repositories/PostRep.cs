
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;

using MailKit.Security;
using MimeKit;
using StepMaster.Services.Interfaces;
using Google.Apis.Util.Store;
using System.Net.Mail;
using System.Net;
namespace StepMaster.Services.Repositories;


public class PostRep:IPost_Service
{
    private string MainName = "StepMaster";
    private string MainPassword = "Lionid!135";
    private string MainMail = "tusxapps.company@gmail.com";
    
    //private string MainPassword = "tusxapps_psswrd";
    private static string googleClientId = "434328467243-u08rg0fi7bniv75n6a2n4f0nt1004uj1.apps.googleusercontent.com";
    private static string googleSecret = "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";
    private static string[] scopes = new[] { Google.Apis.Gmail.v1.GmailService.Scope.MailGoogleCom};


    public async Task<bool> SendMessageAsync(string email, string message)
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
            smtp.Credentials = new NetworkCredential("sosiska6742@gmail.com", "pjkenuwossiczoyn");
            await smtp.SendMailAsync(mailMessage);
            return true;

        }
        catch
        {
            return false;
        }
        //try
        //{

        //    UserCredential credential;

        //    using (var stream = new FileStream("key/client_secret_434328467243-1hb02jpg6131qdrfamll69a72ajsgt79.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
        //    {
        //        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.FromStream(stream).Secrets,
        //            new[] { GmailService.Scope.MailGoogleCom },

        //            "user", CancellationToken.None, new FileDataStore("LoginCookieStoredHere"));


        //    }

        //    using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential }))
        //    {
        //        var Message = new MimeMessage();

        //        Message.From.Add(MailboxAddress.Parse(MainMail));
        //        Message.To.Add(MailboxAddress.Parse(email));
        //        Message.Subject = "Ваш код подтверждения: " + message;               
        //        gmailService.Users.Messages.Send(new Message
        //        {
        //            Raw = Base64UrlEncode(Message.ToString())
        //        }, "me").Execute();


        //    };        


        //    return true;

    
    }      
        
    

    public async Task<bool> SendPasswordOnMail(string email, string message,string host)
    {
        try
        {
            MailAddress from = new MailAddress("sosiska6742@gmail.com", MainName);
            MailAddress to = new MailAddress(email);
            MailMessage mailMessage = new MailMessage(from, to);


            mailMessage.Subject = "На вашем аккаунте хотят сменить пароль, для подтверждения нажмите на кнопку.";
            mailMessage.Body = $"<body style=\"width: 100%; background-color: rgb(255, 255, 255); \">\r\n    <div style=\"height: 100px; width: 90%; background-color: rgb(255, 255, 255); padding: 20px;margin-left: auto; margin-right: auto;\">\r\n        <div style=\"background-color: rgb(0, 119, 255); height: 60%; width: 100%;border-radius: 50px;font-size: 24px;text-align: center; padding:0.3%;  \">\r\n            <a href=\"{host}/view/SystemAnswer/AcceptPassword?email={email}&password={message}\" style=\"text-decoration:none; color: black; width: 900px;\" >\r\n                Edit Password\r\n            </a>\r\n        </div>    \r\n    </div>\r\n    \r\n</body>";
            mailMessage.IsBodyHtml = true;


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("sosiska6742@gmail.com", "pjkenuwossiczoyn");
            await smtp.SendMailAsync(mailMessage);
            return true;

        }
        catch
        {
            return false;
        }
        //try
        //{

        //    UserCredential credential = Login(googleClientId, googleSecret, scopes);
        //    using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential }))
        //    {
        //        var Message = new MimeMessage();
        //        Message.From.Add(MailboxAddress.Parse(MainMail));
        //        Message.To.Add(MailboxAddress.Parse(email));
        //        Message.Subject = "На вашем аккаунте хотят сменить пароль, для подтверждения нажмите на кнопку.";
        //        Message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //        {
        //            Text = $"<body style=\"width: 100%; background-color: rgb(255, 255, 255); \">\r\n    <div style=\"height: 100px; width: 90%; background-color: rgb(255, 255, 255); padding: 20px;margin-left: auto; margin-right: auto;\">\r\n        <div style=\"background-color: rgb(0, 119, 255); height: 60%; width: 100%;border-radius: 50px;font-size: 24px;text-align: center; padding:0.3%;  \">\r\n            <a href=\"{host}/view/SystemAnswer/AcceptPassword?email={email}&password={message}\" style=\"text-decoration:none; color: black; width: 900px;\" >\r\n                Edit Password\r\n            </a>\r\n        </div>    \r\n    </div>\r\n    \r\n</body>"
        //        };
        //        gmailService.Users.Messages.Send(new Message
        //        {
        //            Raw = Base64UrlEncode(Message.ToString())
        //        }, "me").Execute();

        //    };           

        //    return true;

        //}
        //catch
        //{
        //    return false;
        //}
    }
    private static UserCredential Login(string clientId, string clientSecret, string[] scopes)
    {
        
        
        ClientSecrets secrets = new ClientSecrets()
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        };
        return GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None).Result;
    }
    private static string Base64UrlEncode(string input)
    {
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        // Special "url-safe" base64 encode.
        return Convert.ToBase64String(inputBytes)
          .Replace('+', '-')
          .Replace('/', '_')
          .Replace("=", "");
    }
}