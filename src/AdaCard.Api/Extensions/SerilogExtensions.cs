using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace AdaCard.Api.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilogApi()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", $"AdaCard API - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
            .WriteTo.Console()
            .CreateLogger();
    }
}
