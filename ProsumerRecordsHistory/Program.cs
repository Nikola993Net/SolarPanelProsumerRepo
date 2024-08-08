using Serilog.Events;
using Serilog;
using Microsoft.EntityFrameworkCore;
using ProsumerRecordsHistory.Db;
using ProsumerRecordsHistory.Interface;
using ProsumerRecordsHistory.Repository;

var name = typeof(Program).Assembly.GetName().Name;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", name)
    .WriteTo.Seq(serverUrl: "http://seq_in_solar:5341")
    .WriteTo.File(Path.Combine("bin/Log", "ProsumerRecordsHistory.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("starting Prosumer Records server.");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();


    // Add services to the container.
    builder.Services.AddDbContext<ProsumerRecordsDbContext>(options =>
    {
        options.UseInMemoryDatabase("ProsumerRecords");
    });
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddScoped<IProsumerRecordsRepository, ProsumerRecordsRepository>();

    builder.Services.AddControllers();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseCors("AllowAllOrigins");


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

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

