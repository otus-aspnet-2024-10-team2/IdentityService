using Serilog;
using System.Reflection;

namespace Auth.API.DI;

public static class ApplicationInitxtensions
{

    private static readonly string assemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "unknownAssembly";

    public static void ApplicationInit(this WebApplicationBuilder builder)
    {
        InitSerilog(builder);
    }

    private static void InitSerilog(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.WithProperty("appName", assemblyName)
            .CreateLogger();
        builder.Host.UseSerilog();
        builder.Services.AddSingleton(Log.Logger);        
    }
}