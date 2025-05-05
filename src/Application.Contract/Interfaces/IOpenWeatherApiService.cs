
using System.Threading;
using System.Threading.Tasks;
using Application.Contract.Models;
using Application.Contract.Models.OpenWeatherDtos;


namespace Application.Contract.Interfaces
{
    public interface IOpenWeatherApiService
    {
        Task<OpenWeatherApiResponseDto> GetWeatherConditionsAsync(string city , CancellationToken cancellationToken);
        Task<AirPollutionResponseDto> GetAirQualityAsync(Coordinate coord , CancellationToken cancellationToken);
    }
    
}