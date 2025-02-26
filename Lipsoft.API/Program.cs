using Lipsoft.API;

var builder = CreateHostBuilder(args);
var host = builder.Build();

await host.RunAsync();

return;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webHostBuilder =>
        {
            webHostBuilder.UseStartup<Startup>();
        });