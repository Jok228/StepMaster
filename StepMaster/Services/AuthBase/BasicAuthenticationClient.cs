using System.Security.Principal;

namespace StepMaster.Services.AuthBase
{
    public class BasicAuthenticationClient : IIdentity
    {
        public string? AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }

        public string? Role { get; set; }
    }
}
