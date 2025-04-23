using Serilog;

namespace TodoApi.Logging
{
    public static class SerilogConfig
    {
        public static void ConfigureLogging(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("serilog.json", optional: true, reloadOnChange: true);
            
            builder.Host.UseSerilog((ctx, config) =>
            {
                config
                    .ReadFrom.Configuration(ctx.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
            });
        }
    }
}
