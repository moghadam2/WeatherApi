
namespace Application.Contract.Models.OpenWeatherDtos
{
    public record WeatherServiceResultDto
    {
        public float TemperatureCelsius { get; set; }
        public int Humidity { get; set; }
        public float WindSpeed { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}