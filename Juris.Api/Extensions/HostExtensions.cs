using Serilog;
using Serilog.Events;

namespace Juris.Api.Extensions;

public static class HostExtensions
{
    public static void ConfigureSerilog(this IHostBuilder host)
    {
        host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File(
                "..\\logs\\log-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information
            )
        );
    }
}