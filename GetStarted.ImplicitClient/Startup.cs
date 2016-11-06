using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(GetStarted.ImplicitClient.Startup))]

namespace GetStarted.ImplicitClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            var options = new OpenIdConnectAuthenticationOptions
            {
                ClientId = "getstartedimplicit",
                Authority = "http://localhost:53189/",
                RedirectUri = "http://localhost:64584/",
                PostLogoutRedirectUri = "http://localhost:64584/",
                ResponseType = "id_token token",
                Scope = "openid profile",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = notification =>
                    {
                        var identity = notification.AuthenticationTicket.Identity;
                        identity.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));
                        identity.AddClaim(new Claim("access_token", notification.ProtocolMessage.AccessToken));

                        notification.AuthenticationTicket = new AuthenticationTicket(identity, notification.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = notification =>
                    {
                        if (notification.ProtocolMessage.RequestType != OpenIdConnectRequestType.LogoutRequest)
                        {
                            return Task.FromResult(0);
                        }

                        notification.ProtocolMessage.IdTokenHint = notification.OwinContext.Authentication.User.FindFirst("id_token").Value;
                        return Task.FromResult(0);
                    }
                }
            };

            app.UseOpenIdConnectAuthentication(options);
        }
    }
}
