using Serilog;

namespace Juris.Api.Extensions;

public static class HostExtensions
{
    public static void ConfigureSerilog(this IHostBuilder host)
    {
        host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
        );
    }
}