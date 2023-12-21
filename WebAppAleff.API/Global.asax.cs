using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
using Serilog.Events;

namespace WebAppAleff.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {


            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    Path.Combine(AppContext.BaseDirectory, 
                    string.Format("log\\{0}.txt", DateTime.Now.ToString("yyyyMMdd"))))
                .CreateLogger();


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          
        }

        protected void Application_End()
        {
            Log.CloseAndFlush();
        }
    }

}
