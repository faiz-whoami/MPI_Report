using AutoMapper;
using MPI_Report.Mapping;

namespace MPI_Report.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper RegisterMappings()
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        }
    }
}
