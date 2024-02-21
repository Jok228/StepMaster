﻿using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using StepMaster.Models.HashSup;
using StepMaster.Services.ForDb.Interfaces;


namespace API.Auth.AuthBase
{
    public class BasicAunteficationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUser_Service _userService;
        public BasicAunteficationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUser_Service user) : base(options, logger, encoder, clock)
        {
            _userService = user;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            // No authorization header, so throw no result.
            if (!Request.Headers.ContainsKey("Authorization"))
            {

                return await Task.FromResult(AuthenticateResult.Fail("Missing Authorization header"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();




            // If authorization header doesn't start with basic, throw no result.
            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return await Task.FromResult(AuthenticateResult.Fail("Authorization header does not start with 'Basic'"));
            }

            // Decrypt the authorization header and split out the client id/secret which is separated by the first ':'

            var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
            var authSplit = authBase64Decoded.Split(new[] { ':' }, 2);

            // No username and password, so throw no result.
            if (authSplit.Length != 2)
            {
                return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format"));
            }

            // Store the client ID and secret
            var clientId = authSplit[0];
            var clientSecret = authSplit[1];

            // Client ID and secret are incorrect
            var user = await  _userService.GetByLoginAsync(clientId);
            if (user == null)
            {
                return await Task.FromResult(AuthenticateResult.Fail(string.Format("User not found '{0}'", clientId)));
            }
            if (HashCoder.Verify(passwordHash: user.Password, clientSecret))
            {
                var nameIndentity = user.Email;
                // Authenicate the client using basic authentication
                var client = new BasicAuthenticationClient
                {
                    AuthenticationType = "Basic",
                    IsAuthenticated = true,
                    Name = nameIndentity

                };

                // Set the client ID as the name claim type.
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
                {
                    new Claim( ClaimTypes.Name, nameIndentity),
                    new Claim( ClaimTypes.Role, user.Role),
                    new Claim( ClaimTypes.Hash,clientSecret )
                }));

                // Return a success result.
                return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
            }
            else
            {
                return await Task.FromResult(AuthenticateResult.Fail(string.Format("The secret is incorrect for the client '{0}'", clientId)));
            }
        }

    }
}
