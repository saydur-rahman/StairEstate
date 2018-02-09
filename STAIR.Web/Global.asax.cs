﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace STAIR.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Run();

            log4net.Config.XmlConfigurator.Configure();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown  

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs  
        }

        void Session_Start(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] == null)
            {
                var tmpURi = Request.Url;
                var tmpPort = (tmpURi.Port > 0) ? ":" + tmpURi.Port : "";
                var rootUrl = tmpURi.Scheme + "://" + tmpURi.Host + tmpPort + "/";
                if (!tmpURi.AbsoluteUri.Equals(rootUrl))
                {
                    Response.Redirect("/Account/VeiwLoginAfterSessionExpire/");
                }
            }
        }
        
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  
        }
    }
}
