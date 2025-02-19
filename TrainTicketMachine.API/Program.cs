using TrainTicketMachine.Application.Contracts;
using TrainTicketMachine.Application.Services;
using TrainTicketMachine.Application.Helpers;
using TrainTicketMachine.Infrastructure.Caching;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Providers;
using TrainTicketMachine.Infrastructure.Repositories;
using TrainTicketMachine.Infrastructure.BackgroundServices;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
