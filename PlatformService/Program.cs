using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.IsProduction())
{
    Console.WriteLine("---> Using Azure SQL Edge");
    builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
} else 
{
    Console.WriteLine("---> Using InMemory Db");
    builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseInMemoryDatabase("InMemory"));
}

// Add services to the container.


builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<ICommandDataClient,HttpCommandDataClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Console.WriteLine($"--> Command endpoint {IConfiguration["CommandService"]}");

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

PrepDb.PrepPopulation(app, builder.Environment.IsProduction());

app.Run();
