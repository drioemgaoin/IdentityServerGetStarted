﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1;

namespace GetStarted.ImplicitClient
{
    public class MvcApplication : System.Web.HttpApplication
    {   
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
