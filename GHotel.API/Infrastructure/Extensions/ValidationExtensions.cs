using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace GHotel.API.Infrastructure.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection ConfigureFluentValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        return services;
    }
}
