using Application.Contract;
using Application.Contract.Interfaces;
using Application.Contract.Models.OpenWeatherModels;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();



builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("WeatherApi"));
builder.Services.AddTransient<IOpenWeatherApiService, OpenWeatherApiService>();
builder.Services.AddTransient<IWeatherService, WeatherService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseRouting();
app.Run();

public partial class Program { }