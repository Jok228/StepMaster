using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace StepMaster.Initialization.FireBase
{
    public static class ReadFireBaseAdminSDK
    {
        public static async void ReadFireBaseAdminSdk()
        {
            var stream = File.OpenRead("firebasesettings.json");
            var reader = new StreamReader(stream);

            var jsonContent = reader.ReadToEnd();


            if (FirebaseMessaging.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromJson(jsonContent)
                });
            }
        }
    }
}
