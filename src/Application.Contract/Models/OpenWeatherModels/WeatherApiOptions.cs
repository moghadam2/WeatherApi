

namespace Application.Contract.Models.OpenWeatherModels
{
   public record WeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;

}
}