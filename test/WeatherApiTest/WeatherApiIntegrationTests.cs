using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract.Interfaces;
using Application.Contract.Models;
using Application.Contract.Models.OpenWeatherDtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace WeatherApi.Tests
{

    public class WeatherApiIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOpenWeatherApiService> _weatherApiMock;

        public WeatherApiIntegrationTests(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _weatherApiMock = factory.WeatherApiMock;
        }

        [Fact]
        public async Task GetWeather_Should_Return_Valid_Result()
        {
            // Arrange
            var city = "Tehran";
            var coordinate = new Coordinate { Latitude = 35.0, Longitude = 51.0 };
            var Pollution = new AirPollutionDataDto(new AirQualityParameter(AirQualityIndexEnum.Good),
                                                             new Components(0.3, 0.1, 0.1, 0.3, 0.02, 10, 20, 0.05));

            var expectedResponse = new OpenWeatherApiResponseDto
            {
                Coordinate = coordinate,
                Parameter = new WeatherParameter { Temp = 22, Humidity = 40 },
                Wind = new Wind { Speed = 5 }
            };

            var pollutionResponse = new AirPollutionResponseDto(coordinate, new List<AirPollutionDataDto> { Pollution });

            _weatherApiMock
                          .Setup(s => s.GetWeatherConditionsAsync(city, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedResponse);
           
            _weatherApiMock
                          .Setup(s => s.GetAirQualityAsync(coordinate, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(pollutionResponse);


            // Act
            var response = await _httpClient.GetAsync($"/api/Weather/{city}");
            var strResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherResultDto>(strResult);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Assert.Equal(result.AQI, "Good");
            Assert.Equal(result.Temperature.Value, 22);
            Assert.Equal(result.WindSpeed.Value, 5);
            Assert.Equal(result.Humidity.Value, 40);

        }


        [Fact]
        public async Task GetWeather_Should_Return_500_If_Service_Throws()
        {
            // Arrange
            var city = "TestCity";



            // Act
            var response = await _httpClient.GetAsync($"/api/Weather/{city}");


            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        }
    }


}
