using GHotel.Application.Mapping;
using GHotel.Application.Models;
using GHotel.Domain.Entities;
using Mapster;

namespace GHotel.API.Infrastructure.Mapping;
public static class MappingConfiguration
{
    public static void ConfigureMapping(this IServiceCollection services)
    {
        MapsterConfiguration.RegisterMaps();
        ConfigureMappingForBusiness();
    }

    public static void ConfigureMappingForBusiness()
    {
        //TypeAdapterConfig<Project,ProjectResponseModel>
        //    .NewConfig()
        //    .Map(prm => prm.ImageUrl, pr => pr.Im)
    }
}
