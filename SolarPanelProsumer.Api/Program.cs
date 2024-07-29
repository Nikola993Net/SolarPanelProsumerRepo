using SolarPanelProsumer.Api.Interafaces;
using SolarPanelProsumer.Api.Repository;
using Serilog;
using Serilog.Events;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using SolarPanelProsumer.Api.Integrations;
using Microsoft.OpenApi.Models;

var name = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", name)
    .WriteTo.Seq(serverUrl: "http://seq_in_solar:5341")
    //.WriteTo.Console()
    .WriteTo.File(Path.Combine("bin/Log", "SolarPanelProsumerAPI.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// FOR logging different log level we can use it: 
//WriteTo.Logger(lc => lc
//    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
//    .WriteTo.File("InformationLogs.txt"))
//.WriteTo.Logger(lc => lc
//    .Filter.ByExcluding(evt => evt.Level == LogEventLevel.Information)
//    .WriteTo.Seq("http://localhost:5341"))
try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddScoped<IProsumerRepository, ProsumerRepository>();
    builder.Services.AddScoped<ISolarPanelRepository, SolarPanelRepository>();
    builder.Services.AddSingleton<ISunnyHoursProcessingNotification, SunnyHoursProcessingNotification>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen( c=>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolarPanelProsumer.Api", Version = "v1", Description = "Practice project" });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SolarPanelProsumer.Api v1"));
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

