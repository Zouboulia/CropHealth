using Microsoft.EntityFrameworkCore;
using SmartCrop.Data;
using SmartCrop.Shared.Models;

namespace SmartCrop.Services;

public class MonitoringService
{
    private readonly SmartCropDbContext _dbContext;
    private readonly WeatherService _weatherService;

    public MonitoringService(SmartCropDbContext dbContext, WeatherService weatherService)
    {
        _dbContext = dbContext;
        _weatherService = weatherService;
    }

    public async Task<List<Recommendation>> GenerateRecommendationsAsync(string city)
    {
        var recommendations = new List<Recommendation>();

        // Get all fields where the location matches the entered city
        var fields = await _dbContext.Fields
            .Where(f => f.Location.ToLower() == city.ToLower())
            .ToListAsync();

        if (fields.Count == 0)
            return recommendations;

        // Fetch weather data for the city
        var weather = await _weatherService.GetWeatherAsync(city);
        if (weather == null)
            return recommendations;

        foreach (var field in fields)
        {
            var crops = await _dbContext.Crops
                .Where(c => c.FieldId == field.FieldId)
                .ToListAsync();

            var soil = await _dbContext.SoilData
                .FirstOrDefaultAsync(s => s.FieldId == field.FieldId);

            foreach (var crop in crops)
            {
                // Weather-based recommendation
                if (weather.Rain < 10)
                {
                    recommendations.Add(new Recommendation
                    {
                        CropId = crop.CropId,
                        RecommendationText = $"Low rainfall in {city}. Consider irrigating {crop.Name}.",
                        DateIssued = DateTime.Now,
                        Priority = "High",
                        WeatherSummary = $"Rain: {weather.Rain} mm, Temp: {weather.Temperature} °C"
                    });
                }

                // Soil pH recommendation
                if (double.TryParse(soil?.SoilpH, out double pH))
                {
                    if (pH < 5.5)
                    {
                        recommendations.Add(new Recommendation
                        {
                            CropId = crop.CropId,
                            RecommendationText = $"{crop.Name}: Soil is too acidic. Apply lime.",
                            DateIssued = DateTime.Now,
                            Priority = "Medium",
                            WeatherSummary = $"pH: {pH}"
                        });
                    }
                    else if (pH > 7.5)
                    {
                        recommendations.Add(new Recommendation
                        {
                            CropId = crop.CropId,
                            RecommendationText = $"{crop.Name}: Soil is too alkaline. Add sulfur.",
                            DateIssued = DateTime.Now,
                            Priority = "Medium",
                            WeatherSummary = $"pH: {pH}"
                        });
                    }
                }

                // Soil Organic Matter
                if (double.TryParse(soil?.SoilOrganicMatter, out double som) && som < 2.0)
                {
                    recommendations.Add(new Recommendation
                    {
                        CropId = crop.CropId,
                        RecommendationText = $"{crop.Name}: Low organic matter. Add compost.",
                        DateIssued = DateTime.Now,
                        Priority = "Low",
                        WeatherSummary = $"SOM: {som}"
                    });
                }
            }
        }

        return recommendations;
    }
}