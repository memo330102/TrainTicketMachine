using TrainTicketMachine.Application;
using TrainTicketMachine.Domain.Interfaces.Application;
using TrainTicketMachine.Domain.Interfaces.Infrastructure;
using TrainTicketMachine.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StationRepository>(builder.Configuration.GetSection("StationData"));
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IStationService, StationService>();

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
