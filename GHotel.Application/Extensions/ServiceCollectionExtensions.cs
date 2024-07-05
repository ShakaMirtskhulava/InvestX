using GHotel.Application.Services.Business;
using GHotel.Application.Services.Image;
using GHotel.Application.Services.Project;
using GHotel.Application.Services.Share;
using GHotel.Application.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GHotel.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBusinessService, BusinessService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IShareService, ShareService>();

        services.AddScoped<IImageService, ImageService>();
    }
}
