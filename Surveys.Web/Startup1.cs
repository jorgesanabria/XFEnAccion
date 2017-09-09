using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Surveys.Web.App_Start;

[assembly: OwinStartup(typeof(Surveys.Web.Startup1))]

namespace Surveys.Web
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            OAuthConfig.ConfigureOAuth(app, config);
            app.UseWebApi(config);
            WebApiConfig.Register(config);
        }
    }
}
