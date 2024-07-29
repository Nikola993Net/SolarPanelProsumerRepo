using Serilog;
using SolarPanelProsumer.App;

var name = typeof(Program).Assembly.GetName().Name;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Assemblt", name)
    .Enrich.WithMachineName()
    .WriteTo.Seq(serverUrl: "http://seq_in_solar:5341")
    .WriteTo.File(Path.Combine("bin/Log", "SolarPanelProsumerAPP.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Srating worker service");
    var builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<Worker>();


    var host = builder.Build();
    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Ther worker service is terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

