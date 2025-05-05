using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract;
using Application.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;

    }

    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherResultDto>> GetWeather(string city, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _weatherService.GetWeatherAsync(city, cancellationToken);
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex.Message, ex.InnerException?.Message ?? "");
            return BadRequest(ex.InnerException?.Message ?? ex.Message);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex.InnerException?.Message ?? "");
            return StatusCode(500, "An unexpected error occurred.");
        }
       
    }
}