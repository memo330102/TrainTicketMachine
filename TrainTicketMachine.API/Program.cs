using TrainTicketMachine.Application.Contracts;
using TrainTicketMachine.Application.Services;
using TrainTicketMachine.Application.Helpers;
using TrainTicketMachine.Infrastructure.Caching;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Providers;
using TrainTicketMachine.Infrastructure.Repositories;
using TrainTicketMachine.Infrastructure.BackgroundServices;
using TrainTicketMachine.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.Configure<StationRepository>(builder.Configuration.GetSection("StationData"));

builder.Services.AddScoped<IStationDataSource, JsonStationProvider>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IStationCacheService, StationCacheService>();

builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<IStationHelper, StationHelper>();

builder.Services.AddHostedService<StationCacheRefresher>();

builder.Services.AddControllers();

#region Services.Swagger.ApiVersioning
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
