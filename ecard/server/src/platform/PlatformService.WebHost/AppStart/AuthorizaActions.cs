using Abp.Dependency;
using Abp.Extensions;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using PlatformService.BridgeComponent.Authorization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Host.AppStart
{
    public class AuthorizaActions
    {
        public static Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == Clients.Client1.Id)
            {
                context.Validated(Clients.Client1.RedirectUrl);
            }
            else if (context.ClientId == Clients.Client2.Id)
            {
                context.Validated(Clients.Client2.RedirectUrl);
            }

            return Task.FromResult(0);
        }

        public static Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret) ||
                context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                var validateResult = false;
                var grant_type = context.Parameters.Get("grant_type");
                if (!grant_type.IsNullOrEmpty())
                {
                    if (grant_type.Equals("refresh_token"))
                    {
                        validateResult = true;
                    }
                    
                    if (grant_type.Equals("ssoLogin") && IocManager.Instance.IsRegistered<ISSOAuthorization>() &&
                        IocManager.Instance.Resolve<ISSOAuthorization>().CheckTicket(context.Parameters.Get("token"), context.Parameters.Get("username")))
                    {
                        validateResult = true;
                    }

                    if (grant_type.Equals("password") && IocManager.Instance.IsRegistered<IPasswordAuthorization>() &&
                        IocManager.Instance.Resolve<IPasswordAuthorization>().CheckAuthentication(context.Parameters.Get("username"), context.Parameters.Get("password")))
                    {
                        validateResult = true;
                    }

                    if (grant_type.Equals("client_credentials") && IocManager.Instance.IsRegistered<IClientAuthorization>() &&
                        IocManager.Instance.Resolve<IClientAuthorization>().CheckAuthentication(clientId, clientSecret))
                    {
                        validateResult = true;
                    }
                }

                if (validateResult)
                    context.Validated();
            }

            return Task.FromResult(0);
        }

        public static Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(context.UserName, OAuthDefaults.AuthenticationType), context.Scope.Select(x => new Claim("password", x)));

            context.Validated(identity);

            return Task.FromResult(0);
        }

        public static Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType), context.Scope.Select(x => new Claim("client", x)));

            context.Validated(identity);

            return Task.FromResult(0);
        }

        public static Task GrantCustomCredetails(OAuthGrantCustomExtensionContext context)
        {
            var userName = context.Parameters.Get("username");
            var identity = new ClaimsIdentity(new GenericIdentity(userName, OAuthDefaults.AuthenticationType), new Claim[] { new Claim("ssoLogin", "") });

            context.Validated(identity);

            return Task.FromResult(0);
        }

        private static readonly ConcurrentDictionary<string, string> _authenticationCodes =
            new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        public static void CreateAuthenticationCode(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }

        public static void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authenticationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        public static void CreateRefreshToken(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
        }

        public static void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }

    public static class Clients
    {
        public readonly static Client Client1 = new Client
        {
            Id = "123456",
            Secret = "abcdef",
            RedirectUrl = Paths.AuthorizeCodeCallBackPath
        };

        public readonly static Client Client2 = new Client
        {
            Id = "7890ab",
            Secret = "7890ab",
            RedirectUrl = Paths.ImplicitGrantCallBackPath
        };
    }

    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string RedirectUrl { get; set; }
    }

    public static class Paths
    {
        /// <summary>
        /// AuthorizationServer project should run on this URL
        /// </summary>
        public const string AuthorizationServerBaseAddress = "http://localhost:11625";

        /// <summary>
        /// ResourceServer project should run on this URL
        /// </summary>
        public const string ResourceServerBaseAddress = "http://localhost:38385";

        /// <summary>
        /// ImplicitGrant project should be running on this specific port '38515'
        /// </summary>
        public const string ImplicitGrantCallBackPath = "http://localhost:38515/Home/SignIn";

        /// <summary>
        /// AuthorizationCodeGrant project should be running on this URL.
        /// </summary>
        public const string AuthorizeCodeCallBackPath = "http://localhost:38500/";

        public const string AuthorizePath = "/Api/Authorization/Authorize";
        public const string TokenPath = "/Api/Authorization/Token";
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";
        public const string MePath = "/api/Me";
    }
}
