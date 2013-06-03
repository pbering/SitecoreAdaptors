using System;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Sitecore.Diagnostics;
using Sitecore.Web;
using SitecoreAdaptors.Website.App_Start;

namespace SitecoreAdaptors.Website
{
    public class MvcApplication : Application
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
        {
            var frameworkVersion = GetFrameworkVersion();

            if (!string.IsNullOrEmpty(frameworkVersion) && frameworkVersion.StartsWith("v4.", StringComparison.InvariantCultureIgnoreCase))
            {
                args.User = Sitecore.Context.User;
            }
        }

        private string GetFrameworkVersion()
        {
            try
            {
                return RuntimeEnvironment.GetSystemVersion();
            }
            catch (Exception ex)
            {
                Log.Error("Cannot get framework version", ex, this);

                return string.Empty;
            }
        }
    }
}