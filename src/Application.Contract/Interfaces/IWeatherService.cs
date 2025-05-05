using System.Threading;
using System.Threading.Tasks;
using Application.Contract.Models;

namespace Application.Contract;

public interface IWeatherService
{
    Task<WeatherResultDto> GetWeatherAsync(string city , CancellationToken cancellationToken);
}