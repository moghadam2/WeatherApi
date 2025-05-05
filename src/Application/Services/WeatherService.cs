

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract;
using Application.Contract.Interfaces;
using Application.Contract.Models;
using Application.Contract.Models.OpenWeatherDtos;

namespace Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IOpenWeatherApiService _weatherApiService;


    public WeatherService(IOpenWeatherApiService weatherApi)
    {
        _weatherApiService = weatherApi;
    }

    public async Task<WeatherResultDto> GetWeatherAsync(string city, CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(city))
            throw new ApplicationException( "City is null" , new Exception("The input parameter city cannot be empty"));

        var cityWeather = await _weatherApiService.GetWeatherConditionsAsync(city, cancellationToken);

        if (cityWeather.Coordinate?.Latitude == 0 && cityWeather.Coordinate?.Longitude == 0 || cityWeather.Coordinate is null )
            throw new ApplicationException("The coordinates are not available in the weather service output." ,
                                            new Exception("The input parameter coordinate cannot be empty or zero"));
      
        var cityAirPollutions = await _weatherApiService.GetAirQualityAsync(cityWeather.Coordinate, cancellationToken);
       
        var cityAirPollution = cityAirPollutions.List.FirstOrDefault();// The list of response is based on time.
        WeatherResultDto res = BuildResult(cityWeather, cityAirPollution);

        return res;

    }

    private static WeatherResultDto BuildResult(OpenWeatherApiResponseDto cityWeather, AirPollutionDataDto cityAirPollution)
    {
        return new WeatherResultBuilder()
            .WithTemperature(cityWeather.Parameter.Temp)
            .WithHumidity(cityWeather.Parameter.Humidity)
            .WithWindSpeed(cityWeather.Wind.Speed)
            .WithCoordinate(cityWeather.Coordinate.Latitude, cityWeather.Coordinate.Longitude)
            .WithAQI(cityAirPollution is null ? AirQualityIndexEnum.NotExist.ToString() : cityAirPollution.QualityParameter.aqi.ToString())
            .WithPollutants(cityAirPollution != null
                ? new AirPollutants
                {
                    CO = cityAirPollution.Components.co,
                    NO = cityAirPollution.Components.no,
                    NO2 = cityAirPollution.Components.no2,
                    O3 = cityAirPollution.Components.o3,
                    SO2 = cityAirPollution.Components.so2,
                    PM2_5 = cityAirPollution.Components.pm2_5,
                    PM10 = cityAirPollution.Components.pm10,
                    NH3 = cityAirPollution.Components.nh3
                }
                : null)
            .Build();
    }

}



