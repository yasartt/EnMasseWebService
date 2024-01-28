using EnMasseWebService.Models;
using EnMasseWebService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRChat;
using EnMasseWebService.Models.Entities;
using EnMasseWebService.Models.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CafeService>();
builder.Services.AddScoped<DailyService>();

builder.Services.Configure<DailyImageDatabaseSettings>(
    builder.Configuration.GetSection("DailyImageDatabase"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, CafeUser>>( opt => new Dictionary<string, CafeUser>());

// Access the Configuration object through the HostingEnvironment
var configuration = builder.Configuration;

// Register DbContext here
builder.Services.AddDbContext<EnteractDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Register MongoDB client and configure settings
builder.Services.Configure<DailyImageDatabaseSettings>(
    builder.Configuration.GetSection("DailyImageDatabase"));

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<DailyImageDatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Register IMongoDatabase and IMongoCollection<ImageDTO>
builder.Services.AddScoped(serviceProvider =>
{
    var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
    var settings = serviceProvider.GetRequiredService<IOptions<DailyImageDatabaseSettings>>().Value;
    var database = mongoClient.GetDatabase(settings.DatabaseName);
    return database.GetCollection<ImageDTO>(settings.DailyImagesCollectionName);
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CafeHub>("/cafeHub");

app.Run();