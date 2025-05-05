using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Contract.Models.OpenWeatherDtos
{
    
public record AirPollutionResponseDto(
    
    [property: JsonPropertyName("coord")] 
    Coordinate Coordinate,

    [property: JsonPropertyName("list")] 
    IEnumerable<AirPollutionDataDto> List
);



public record AirPollutionDataDto(
    
    [property: JsonPropertyName("main")] 
    AirQualityParameter QualityParameter,
    

    [property: JsonPropertyName("components")] 
    Components Components

);

public record AirQualityParameter(
    
    [property: JsonPropertyName("aqi")] 
    AirQualityIndexEnum aqi
);

public enum AirQualityIndexEnum
{   
    NotExist = 0, // Default
    Good = 1 ,
    Fair = 2 , 
    Moderate = 3 , 
    Poor = 4 ,
    VeryPoor = 5 ,
}

public record Components(
    double co,
    double no,
    double no2,
    double o3,
    double so2,
    double pm2_5,
    double pm10,
    double nh3
);

}