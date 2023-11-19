
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using StepMaster.Services.Interfaces;
namespace StepMaster.Services.Repositories;


public class PostRep:IPost_Service
{
    private string MainMail = "tusxapps.company@gmail.com";
    
    private string MainPassword = "tusxapps_psswrd";
    private static string googleClientId = "434328467243-u08rg0fi7bniv75n6a2n4f0nt1004uj1.apps.googleusercontent.com";
    private static string googleSecret = "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";
    private static string[] scopes = new[] { Google.Apis.Gmail.v1.GmailService.Scope.MailGoogleCom};


    public async Task<bool> SendMessageAsync(string email,string message)
    {
        try
        {
            UserCredential credential = Login(googleClientId, googleSecret, scopes);

            using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential }))
            {
                var Message = new MimeMessage();

                Message.From.Add(MailboxAddress.Parse(MainMail));
                Message.To.Add(MailboxAddress.Parse(email));
                Message.Subject = "Ваш код подтверждения: " + message;               
                gmailService.Users.Messages.Send(new Message
                {
                    Raw = Base64UrlEncode(Message.ToString())
                }, "me").Execute();


            };        

           
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
            UserCredential credential = Login(googleClientId, googleSecret, scopes);

            using (var gmailService = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential }))
            {
                var Message = new MimeMessage();
                Message.From.Add(MailboxAddress.Parse(MainMail));
                Message.To.Add(MailboxAddress.Parse(email));
                Message.Subject = "На вашем аккаунте хотят сменить пароль, для подтверждения нажмите на кнопку.";
                Message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"<body style=\"width: 100%; background-color: rgb(255, 255, 255); \">\r\n    <div style=\"height: 100px; width: 90%; background-color: rgb(255, 255, 255); padding: 20px;margin-left: auto; margin-right: auto;\">\r\n        <div style=\"background-color: rgb(0, 119, 255); height: 60%; width: 100%;border-radius: 50px;font-size: 24px;text-align: center; padding:0.3%;  \">\r\n            <a href=\"{host}/view/SystemAnswer/AcceptPassword?email={email}&password={message}\" style=\"text-decoration:none; color: black; width: 900px;\" >\r\n                Edit Password\r\n            </a>\r\n        </div>    \r\n    </div>\r\n    \r\n</body>"
                };
                gmailService.Users.Messages.Send(new Message
                {
                    Raw = Base64UrlEncode(Message.ToString())
                }, "me").Execute();

            };           
            
            return true;

        }
        catch
        {
            return false;
        }
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