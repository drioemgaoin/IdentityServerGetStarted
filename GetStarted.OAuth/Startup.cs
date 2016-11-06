using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GetStarted.OAuth.Startup))]

namespace GetStarted.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(InMemoryManager.GetClients())
                .UseInMemoryScopes(InMemoryManager.GetScopes())
                .UseInMemoryUsers(InMemoryManager.GetUsers());

            var fullPath = HttpContext.Current.Server.MapPath("~/Certificate/localhost.pfx");
            var certificate = new X509Certificate2(fullPath, "password", X509KeyStorageFlags.MachineKeySet); ;
            var options = new IdentityServerOptions
            {
                SigningCertificate = certificate,
                RequireSsl = false,
                Factory = factory,
                AuthenticationOptions = new AuthenticationOptions
                {
                    EnableSignOutPrompt = false,
                    EnablePostSignOutAutoRedirect = true
                },
                LoggingOptions = new LoggingOptions
                {
                    EnableHttpLogging = true,
                    EnableWebApiDiagnostics = true,
                    EnableKatanaLogging = true,
                    WebApiDiagnosticsIsVerbose = true
                }
            };

            app.UseIdentityServer(options);    
        }
    }
}
