using IdentityServer3.AccessTokenValidation;
using Owin;

namespace GetStarted.Api.App_Start
{
    public static class OAuthConfig
    {
        public static IAppBuilder UseOAuth(this IAppBuilder app)
        {
            var options = new IdentityServerBearerTokenAuthenticationOptions()
            {
                Authority = "http://localhost:53189/", // Address of the Authorization Server
            };

            app.UseIdentityServerBearerTokenAuthentication(options);

            return app;
        }
    }
}