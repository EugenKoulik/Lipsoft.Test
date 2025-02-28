using System.Text.Json.Serialization;
using Lipsoft.API.Middleware;
using Lipsoft.API.Swagger;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services;
using Lipsoft.Data;
using Lipsoft.Data.Implementations;
using Lipsoft.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(json => json.JsonSerializerOptions.Converters.Insert(0, new JsonStringEnumConverter()))
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = _ =>
                    new BadRequestObjectResult("Передана невалидная модель для сервера");
            });

        services.Configure<DbSettings>(configuration.GetSection("DbSettings"));
        services.ConfigureSwagger();
        
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientRepository, ClientRepository>();

        services.AddScoped<ICallService, CallService>();
        services.AddScoped<ICallRepository, CallRepository>();

        services.AddScoped<ICreditApplicationService, CreditApplicationService>();
        services.AddScoped<ICreditApplicationRepository, CreditApplicationRepository>();

        services.AddScoped<ICreditProductService, CreditProductService>();
        services.AddScoped<ICreditProductRepository, CreditProductRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMiddleware<ExceptionsHandling>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}