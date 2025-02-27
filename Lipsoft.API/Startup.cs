using System.Text.Json.Serialization;
using Lipsoft.API.Middleware;
using Lipsoft.API.Swagger;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services;
using Lipsoft.Data.Implementations;
using Lipsoft.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lipsoft.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
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
        
        var connectionString = _configuration.GetConnectionString("DbConnection");
        
        services.ConfigureSwagger();
        
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientRepository>(_ => new ClientRepository(connectionString));

        services.AddScoped<ICallService, CallService>();
        services.AddScoped<ICallRepository>(_ => new CallRepository(connectionString));

        services.AddScoped<ICreditApplicationService, CreditApplicationService>();
        services.AddScoped<ICreditApplicationRepository>(_ => new CreditApplicationRepository(connectionString));

        services.AddScoped<ICreditProductService, CreditProductService>();
        services.AddScoped<ICreditProductRepository>(_ => new CreditProductRepository(connectionString));
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