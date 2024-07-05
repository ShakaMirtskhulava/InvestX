using GHotel.API.Infrastructure.Extensions;
using GHotel.API.Infrastructure.Mapping;
using GHotel.Application.Extensions;
using GHotel.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureLocalization(builder.Configuration);
builder.ConfigureLogging();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureFluentValidations();
builder.Services.ConfigureMapping();

var app = builder.Build();

app
   .UseCORS()
   .UseLocalization()
   .UseGlobalExceptionHandling()
   .UseRequestResponseLogging();

app.UseSwaggerApiVersioning();
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
