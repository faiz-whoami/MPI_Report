using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MPI_Report.App_Start;
using MPI_Report.Data;
using MPI_Report.Migrations;
using Ninject;
using Ninject.Web.Mvc;

namespace MPI_Report
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            IKernel kernel = NinjectConfig.RegisterServices();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
