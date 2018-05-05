using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using CallCenter.DAL.Core;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common.OwinHost;
using CallCenter.API.App_Start;
using Ninject.Web.WebApi.OwinHost;
using System.Reflection;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using System.Linq;
using CallCenter.API.Filters;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(CallCenter.API.Startup))]

namespace CallCenter.API
{
    public class Startup
    {
        //[Inject]
        //public SimpleAuthorizationServerProvider authProvider { get; set; }

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = GlobalConfiguration.Configuration;

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    //EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
            //config.DependencyResolver = new OwinNinjectDependencyResolver(NinjectConfig.CreateKernel());

            WebApiConfig.Register(config);
            //ConfigureOAuth(app);
            //app.UseWebApi(config);
            config.MessageHandlers.Add(new TokenValidationHandler());
            //ConfigureOAuth(app);

            app.UseNinjectMiddleware(NinjectConfig.CreateKernel).UseNinjectWebApi(config);
            //SignalR
            

            CallCenterContext.InitDb();

            GlobalConfiguration.Configuration.EnsureInitialized();
            
        }
    }
}
