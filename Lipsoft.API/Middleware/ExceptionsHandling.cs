using System.Net.Mime;
using System.Text.Json;

namespace Lipsoft.API.Middleware;

public class ExceptionsHandling(RequestDelegate next)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        var badResponse = new { Error = ex.Message };
        
        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var badResponseJson = JsonSerializer.Serialize(badResponse, JsonSerializerOptions);

        await context.Response.WriteAsync(badResponseJson);
    }
}