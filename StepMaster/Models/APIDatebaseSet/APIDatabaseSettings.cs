namespace StepMaster.Models.APIDatebaseSet
{
    public class ApiDatabaseSettings : IAPIDatabaseSettings
    {
        public string NameCollection { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
