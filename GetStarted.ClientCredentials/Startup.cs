using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GetStarted.ClientCredentials.Startup))]

namespace GetStarted.ClientCredentials
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:53189/"
            };

            app.UseIdentityServerBearerTokenAuthentication(options);

        }
    }
}
