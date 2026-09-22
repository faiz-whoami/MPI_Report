
using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using MPI_Report.Data;
using MPI_Report.Repositories.Implementations;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Services.Implementations;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.App_Start
{
    public static class NinjectConfig
    {
        public static IKernel RegisterServices()
        {
            var kernel = new StandardKernel();

            // Database Context
            kernel.Bind<ApplicationDbContext>()
                  .ToSelf()
                  .InRequestScope();

            // Repositories
            kernel.Bind<ICustomerRepository>()
                  .To<CustomerRepository>()
                  .InRequestScope();

            // Services
            kernel.Bind<ICustomerService>()
                  .To<CustomerService>()
                  .InRequestScope();

            // AutoMapper
            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping.MappingProfile>();
            }).CreateMapper();

            kernel.Bind<IMapper>()
                  .ToConstant(mapper);

            return kernel;
        }
    }
}
