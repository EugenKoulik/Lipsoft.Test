using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lipsoft.API.Swagger;

public static class ServiceCollectionExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        
        services.Configure<SwaggerGenOptions>(
            options =>
            {
                options.DescribeAllParametersInCamelCase();
            }
        );

        services.Configure<SwaggerOptions>(options => { options.RouteTemplate = "swagger/{documentName}/swagger.json"; });
    }
}