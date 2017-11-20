using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SignalR2D2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #if !DEBUG
            GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            #endif

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
