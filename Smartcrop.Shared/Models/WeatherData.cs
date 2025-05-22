namespace SmartCrop.Shared.Models;

public class WeatherData
{
    public int WeatherDataId { get; set; } // Primary key for the WeatherData table
    public int CropId { get; set; } // Foreign key to the Crop table
    public Crop Crop { get; set; } // Navigation property to the Crop table
    
    
    public DateTime Date { get; set; } // Date of the weather data
    public double Temperature { get; set; } // Temperature in Celsius
    public double Humidity { get; set; } // Humidity in percentage
    public double Rain { get; set; } // Rainfall in mm
    public double UVi { get; set; } // UV index
    public double Clouds { get; set; } // Cloud cover in percentage
    
    
    //get weather data from OpenWeatherapp API
    
    // public string GetWeatherData()
    // {
    //     // This method will call the OpenWeatherMap API and return the weather data
    //     // Will use HttpClient to make the API call and deserialize the response
    //     // into a WeatherData object
    //     return "Weather data fetched from OpenWeatherMap API";
    // }
}