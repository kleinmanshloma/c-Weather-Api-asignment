using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();

app.MapGet("/", () => "Hello Earth!");

app.MapGet("/{zipcode}",(string zipcode) => {

  var apiKey = app.Configuration["Weather:ApiKey"]!;
  var client = new WeatherApiClient(apiKey);

  return client.GetWeather(zipcode);
});

app.Run();
