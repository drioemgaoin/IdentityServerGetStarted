using System.Web.Http;
using GetStarted.Api.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GetStarted.Api.Startup))]

namespace GetStarted.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            SwaggerConfig.Register(httpConfiguration);
            WebApiConfig.Register(httpConfiguration);


            app.UseOAuth();
            app.UseWebApi(httpConfiguration);
        }
    }
}
