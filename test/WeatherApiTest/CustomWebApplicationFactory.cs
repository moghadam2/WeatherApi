using System.Linq;
using Application.Contract.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace WeatherApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<IOpenWeatherApiService> WeatherApiMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // mock
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(IOpenWeatherApiService));

            if (descriptor != null)
                services.Remove(descriptor);

            services.TryAddScoped(_ => WeatherApiMock.Object);
        });
    }
}
