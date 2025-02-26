using Lipsoft.API.Middleware;
using Lipsoft.API.Swagger;
using Lipsoft.BLL.Interfaces;
using Lipsoft.BLL.Services;
using Lipsoft.Data.Implementations;
using Lipsoft.Data.Repositories;

namespace Lipsoft.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.ConfigureSwagger();
        
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IClientRepository>(_ => new ClientRepository(string.Empty));
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