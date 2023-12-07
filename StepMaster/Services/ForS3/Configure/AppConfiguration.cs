namespace API.Services.ForS3.Configure
{
    public class AppConfiguration : IAppConfiguration
    {
        
        public string AwsAccessKey { get; set; } = "L7TEHWVRWAINB9YN1LQK";
        public string AwsSecretAccessKey { get; set; } = "pASmqBWmEjBQhJ0eCrhwOxnWsEjEgFdn8rhTGmzk";
        public string URL { get; set; } = "https://obs.ru-moscow-1.hc.sbercloud.ru";
        public string BucketName { get; set; } = "sterpmaster";
    }
}
