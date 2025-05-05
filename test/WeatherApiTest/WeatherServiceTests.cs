
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract.Interfaces;
using Application.Contract.Models;
using Application.Contract.Models.OpenWeatherDtos;
using Application.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace WeatherApiTest
{
    public class WeatherServiceTests
    {

        private readonly Mock<IOpenWeatherApiService> _mockWeatherApiService;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _mockWeatherApiService = new Mock<IOpenWeatherApiService>();
            _weatherService = new WeatherService(_mockWeatherApiService.Object);
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldThrow_WhenCityIsNull()
        {
            // Act
            Func<Task> act = async () => await _weatherService.GetWeatherAsync("", CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ApplicationException>()
                .WithMessage("City is null");
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldThrow_WhenCoordinateIsZero()
        {
            // Arrange
            var cityWeather = new OpenWeatherApiResponseDto
            {
                Parameter = new WeatherParameter { Temp = 20, Humidity = 40 },
                Wind = new Wind { Speed = 5 }
            };


            _mockWeatherApiService
                 .Setup(s => s.GetWeatherConditionsAsync("Tehran", It.IsAny<CancellationToken>()))
                 .ReturnsAsync(cityWeather);


            // Act
            Func<Task> act = async () => await _weatherService.GetWeatherAsync("Tehran", CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ApplicationException>()
                .WithMessage("The coordinates are not available in the weather service output.");

      
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnResult_WhenValidCity()
        {
            // Arrange
            var cityWeather = new OpenWeatherApiResponseDto
            {
                Coordinate = new Coordinate { Latitude = 35.0, Longitude = 51.0 },
                Parameter = new WeatherParameter { Temp = 20, Humidity = 40 },
                Wind = new Wind { Speed = 5 }
            };

            var pollutionData = new AirPollutionDataDto(new AirQualityParameter(AirQualityIndexEnum.Moderate),
                                                         new Components(0.4, 0.1, 0.2, 0.3, 0.1, 0.5, 0.6, 0.7));


            _mockWeatherApiService.Setup(x => x.GetWeatherConditionsAsync("Tehran", It.IsAny<CancellationToken>()))
                .ReturnsAsync(cityWeather);

            _mockWeatherApiService.Setup(x => x.GetAirQualityAsync(cityWeather.Coordinate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AirPollutionResponseDto(cityWeather.Coordinate,
                                                          new List<AirPollutionDataDto> { pollutionData }));

            // Act
            var result = await _weatherService.GetWeatherAsync("Tehran", CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Temperature.Value.Should().Be(20);
            result.Humidity.Value.Should().Be(40);
            result.WindSpeed.Value.Should().Be(5);
            result.MajorPollutants.CO.Should().Be(0.4);
        }
    }
}
