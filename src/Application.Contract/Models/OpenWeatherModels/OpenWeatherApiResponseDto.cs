using System.Text.Json.Serialization;

namespace Application.Contract.Models
{

    public record OpenWeatherApiResponseDto
    {
        [JsonPropertyName("coord")]
        public Coordinate Coordinate { get; set; }

        [JsonPropertyName("main")]
        public WeatherParameter Parameter { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

    }

    public record Coordinate
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }



    public record WeatherParameter
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

    }

    public record Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }


    }

}

