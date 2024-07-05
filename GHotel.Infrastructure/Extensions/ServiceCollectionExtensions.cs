using GHotel.Application.Authentication;
using GHotel.Application.Repositories;
using GHotel.Application.Utilities;
using GHotel.Infrastructure.Authentication;
using GHotel.Infrastructure.Repositories;
using GHotel.Infrastructure.Utilities;
using GHotel.Persistance;
using GHotel.Persistance.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GHotel.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));

        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShareRepository, ShareRepository>();

        services.AddScoped<IMyPasswordHasher, MyPasswordHasher>();

        services.AddScoped<IImageUtility, ImageUtility>();
        services.AddScoped<ILockUtility, SemaphorLockUtility>();

        services.AddDbContext<AppDBContext>();
    }
}
