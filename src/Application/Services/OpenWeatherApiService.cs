
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract.Interfaces;
using Application.Contract.Models;
using Application.Contract.Models.OpenWeatherDtos;
using Application.Contract.Models.OpenWeatherModels;
using Microsoft.Extensions.Options;


namespace Application.Services
{
    public class OpenWeatherApiService : IOpenWeatherApiService
    {
        private readonly IHttpClientFactory _httpclientFactory;
        private readonly IOptions<WeatherApiOptions> _options;

        public OpenWeatherApiService(IOptions<WeatherApiOptions> options, IHttpClientFactory httpclientFactory)
        {
            _options = options;
            _httpclientFactory = httpclientFactory;
        }

        public async Task<AirPollutionResponseDto> GetAirQualityAsync(Coordinate coordinate, CancellationToken cancellationToken)
        {

            var pollutionUrl = $"air_pollution?lat={coordinate.Latitude}&lon={coordinate.Longitude}&appid={_options.Value.ApiKey}";
            var pollutionResponse = await GetClient().GetAsync(pollutionUrl, cancellationToken);


            if (!pollutionResponse.IsSuccessStatusCode)
                throw new ApplicationException($"Pollution API Error: {pollutionResponse.ReasonPhrase}");


            var pollutionResponsAsJson = await pollutionResponse.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<AirPollutionResponseDto>(pollutionResponsAsJson);

            return result;

        }



        public async Task<OpenWeatherApiResponseDto> GetWeatherConditionsAsync(string city, CancellationToken cancellationToken)
        {


            var weatherUrl = $"weather?q={city}&appid={_options.Value.ApiKey}&units=metric&lang=fa";
            var weatherResponse = await GetClient().GetAsync(weatherUrl, cancellationToken);


            if (!weatherResponse.IsSuccessStatusCode)
            {
                if (weatherResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new ApplicationException($"Weather API Error: {weatherResponse.ReasonPhrase}" , new Exception("Cannot Connect To Service"));
                else
                    throw new ApplicationException($"Weather API Error: {weatherResponse.ReasonPhrase}" , new Exception("Not Fond"));

            }



            var weatherResponseAsJson = await weatherResponse.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<OpenWeatherApiResponseDto>(weatherResponseAsJson);

            return result;

        }


        private HttpClient GetClient()
        {
            var client = _httpclientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            client.BaseAddress = new Uri(_options.Value.BaseUrl);
            return client;
        }


    }
}