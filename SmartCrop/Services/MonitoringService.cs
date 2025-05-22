using SmartCrop.Data;
using SmartCrop.Shared.Models;
using SmartCrop.Services;
using Microsoft.EntityFrameworkCore;

public class MonitoringService
{
    private readonly SmartCropDbContext _dbContext;
    private readonly WeatherService _weatherService;

    public MonitoringService(SmartCropDbContext dbContext, WeatherService weatherService)
    {
        _dbContext = dbContext;
        _weatherService = weatherService;
    }

    public async Task<List<Recommendation>> GenerateRecommendationsAsync(int cropId, string cityName)
    {
        var crop = await _dbContext.Crops
            .Include(c => c.Field)
            .FirstOrDefaultAsync(c => c.CropId == cropId);

        if (crop == null)
            return new List<Recommendation>();

        var soilData = await _dbContext.SoilData
            .FirstOrDefaultAsync(s => s.FieldId == crop.FieldId);

        var weather = await _weatherService.GetWeatherAsync(cityName);
        if (weather == null)
            return new List<Recommendation>();

        var recommendations = new List<Recommendation>();

        // Weather-based recommendation
        if (weather.Rain < 10)
        {
            recommendations.Add(new Recommendation
            {
                CropId = cropId,
                RecommendationText = "Low rainfall detected. Consider irrigation.",
                DateIssued = DateTime.Now,
                Priority = "High"
            });
        }

        // Soil pH recommendation
        if (double.TryParse(soilData?.SoilpH, out double pH))
        {
            if (pH < 5.5)
            {
                recommendations.Add(new Recommendation
                {
                    CropId = cropId,
                    RecommendationText = "Soil is too acidic. Apply lime to raise pH.",
                    DateIssued = DateTime.Now,
                    Priority = "Medium"
                });
            }
            else if (pH > 7.5)
            {
                recommendations.Add(new Recommendation
                {
                    CropId = cropId,
                    RecommendationText = "Soil is too alkaline. Consider adding sulfur or acid-forming fertilizers.",
                    DateIssued = DateTime.Now,
                    Priority = "Medium"
                });
            }
        }

        // Soil Organic Matter (SOM) recommendation
        if (double.TryParse(soilData?.SoilOrganicMatter, out double som))
        {
            if (som < 2.0)
            {
                recommendations.Add(new Recommendation
                {
                    CropId = cropId,
                    RecommendationText = "Low organic matter detected. Add compost or green manure to improve soil health.",
                    DateIssued = DateTime.Now,
                    Priority = "Low"
                });
            }
            else if (som > 6.0)
            {
                recommendations.Add(new Recommendation
                {
                    CropId = cropId,
                    RecommendationText = "Organic matter is very high. Monitor for potential nutrient imbalances.",
                    DateIssued = DateTime.Now,
                    Priority = "Low"
                });
            }
        }

        // Add weather summary
        string summary = $"Weather in {cityName}: Temp {weather.Temperature}°C, Rain {weather.Rain}mm, Humidity {weather.Humidity}%, Clouds {weather.Clouds}%";

        foreach (var rec in recommendations)
        {
            rec.WeatherSummary = summary;
        }

        return recommendations;
    }
}
