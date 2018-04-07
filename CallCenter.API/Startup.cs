using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using CallCenter.DAL.Core;
using Microsoft.Owin.Security.OAuth;
using CallCenter.API.Providers;
using Ninject;
using Ninject.Web.Common.OwinHost;
using CallCenter.API.App_Start;
using Ninject.Web.WebApi.OwinHost;
using System.Reflection;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System.Linq;
using CallCenter.API.Filters;

[assembly: OwinStartup(typeof(CallCenter.API.Startup))]

namespace CallCenter.API
{
    public class Startup
    {
        [Inject]
        public SimpleAuthorizationServerProvider authProvider { get; set; }

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = GlobalConfiguration.Configuration;
            //config.DependencyResolver = new OwinNinjectDependencyResolver(NinjectConfig.CreateKernel());

            WebApiConfig.Register(config);
            //ConfigureOAuth(app);
            //app.UseWebApi(config);
            config.MessageHandlers.Add(new TokenValidationHandler());
            //ConfigureOAuth(app);

            app.UseNinjectMiddleware(NinjectConfig.CreateKernel).UseNinjectWebApi(config);
            
            CallCenterContext.InitDb();

            GlobalConfiguration.Configuration.EnsureInitialized();
            
        }

        //public void ConfigureOAuth(IAppBuilder app)
        //{
        //    OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
        //    {
        //        AllowInsecureHttp = true,
        //        TokenEndpointPath = new PathString("/token"),
        //        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        //        Provider = new SimpleAuthorizationServerProvider()
        //    };

        //    // Token Generation
        //    app.UseOAuthAuthorizationServer(OAuthServerOptions);
        //    app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        //}
    }
}
