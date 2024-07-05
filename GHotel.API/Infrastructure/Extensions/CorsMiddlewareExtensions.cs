namespace GHotel.API.Infrastructure.Extensions;

public static class CorsMiddlewareExtensions
{
    public static IApplicationBuilder UseCORS(this IApplicationBuilder app)
    {
        app.UseCors(options =>
        {
            options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        return app;
    }
}
