using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SmartCrop.Shared.Models;

namespace SmartCrop.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;
        private const string ApiKey = "ed9672b6feb8e2f4609252ba65065547";

        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<WeatherData?> GetWeatherAsync(string cityName)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={ApiKey}&units=metric";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Weather API call failed: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var weatherData = new WeatherData
                {
                    Date = DateTime.Now,
                    Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    Humidity = root.GetProperty("main").GetProperty("humidity").GetDouble(),
                    Rain = root.TryGetProperty("rain", out var rainElement) &&
                           rainElement.TryGetProperty("1h", out var rainVal)
                           ? rainVal.GetDouble()
                           : 0,
                    Clouds = root.GetProperty("clouds").GetProperty("all").GetDouble(),
                    UVi = 0 // Note: OpenWeatherMap current weather endpoint does not return UVi
                };

                return weatherData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching weather data.");
                return null;
            }
        }
    }
}
