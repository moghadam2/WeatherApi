using Application.Contract.Models.OpenWeatherDtos;

namespace Application.Contract.Models;

public class WeatherResultDto
{
    public Temperature Temperature { get; set; }
    public Humidity Humidity { get; set; }
    public WindSpeed WindSpeed { get; set; }
    public string AQI { get; set; }
    public AirPollutants MajorPollutants { get; set; } = new();
    public Coordinate Coordinate { get; set; }

}
public record WindSpeed
{
    public double Value { get; }

    public string Unit => "m/s";

    public WindSpeed(double value)
    {
        Value = value;
    }

}

public record Humidity
{
    public double Value { get; }

    public string Unit => "%";

    public Humidity(double value)
    {
        Value = value;
    }

}
public record Temperature
{
    public double Value { get; }

    public string Unit => "°C";

    public Temperature(double value)
    {
        Value = value;
    }
}


public class AirPollutants
{
    public double CO { get; set; }      // Carbon monoxide
    public double NO { get; set; }      // Nitric oxide
    public double NO2 { get; set; }     // Nitrogen dioxide
    public double O3 { get; set; }      // Ozone
    public double SO2 { get; set; }     // Sulfur dioxide
    public double PM2_5 { get; set; }   // Fine particles matter
    public double PM10 { get; set; }    // Coarse particulate matter
    public double NH3 { get; set; }     // Ammonia
}


public class WeatherResultBuilder
{
    private Temperature _temperature;
    private Humidity _humidity;
    private WindSpeed _windSpeed;
    private string _aqi;
    private AirPollutants _pollutants = new();
    private Coordinate _coordinate;

    public WeatherResultBuilder WithTemperature(double temp)
    {
        _temperature = new Temperature(temp);
        return this;
    }

    public WeatherResultBuilder WithHumidity(int humidity)
    {
        _humidity = new Humidity(humidity);
        return this;
    }

    public WeatherResultBuilder WithWindSpeed(double speed)
    {
        _windSpeed = new WindSpeed(speed);
        return this;
    }

    public WeatherResultBuilder WithAQI(string aqi)
    {
        if (string.IsNullOrEmpty(aqi))
            _aqi = AirQualityIndexEnum.NotExist.ToString();

        else
            _aqi = aqi;
       
        return this;
    }

    public WeatherResultBuilder WithPollutants(AirPollutants pollutants)
    {
        _pollutants = pollutants;
        return this;
    }

    public WeatherResultBuilder WithCoordinate(double lat, double lon)
    {
        _coordinate = new Coordinate { Latitude = lat, Longitude = lon };
        return this;
    }

    public WeatherResultDto Build()
    {
        return new WeatherResultDto
        {
            Temperature = _temperature,
            Humidity = _humidity,
            WindSpeed = _windSpeed,
            AQI = _aqi,
            MajorPollutants = _pollutants,
            Coordinate = _coordinate
        };
    }
}