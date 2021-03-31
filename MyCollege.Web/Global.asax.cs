using MyCollege.Data;
using MyCollege.Data.Migrations;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyCollege.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(
            new MigrateDatabaseToLatestVersion<Context, Configuration>("MyCollegeDb"));

            //Database.SetInitializer<Context>(Configuration);
        }
    }
}
