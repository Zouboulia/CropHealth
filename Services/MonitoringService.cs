using Microsoft.EntityFrameworkCore;
using SmartCrop.Data;
using SmartCrop.Shared.Models;

namespace SmartCrop.Services
{
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

            // Get fields where location matches the city
            var fields = await _dbContext.Fields
                .Where(f => f.Location.ToLower() == city.ToLower())
                .ToListAsync();

            if (fields.Count == 0)
                return recommendations;

            // Get weather data for this city
            var weather = await _weatherService.GetWeatherAsync(city);
            if (weather == null)
                return recommendations;

            // Iterate through each field and get crops and soil data
            foreach (var field in fields)
            {
                var crops = await _dbContext.Crops
                    .Where(c => c.FieldId == field.FieldId)
                    .ToListAsync();

                var soil = await _dbContext.SoilData
                    .FirstOrDefaultAsync(s => s.FieldId == field.FieldId);

                // If no crops or soil data, skip this field
                foreach (var crop in crops)
                {
                    // Generate recommendations based on weather and soil data
                    recommendations.AddRange(GenerateWeatherRecommendation(crop, weather, city));
                    recommendations.AddRange(GenerateSoilPhRecommendation(crop, soil));
                    recommendations.AddRange(GenerateSoilOrganicMatterRecommendation(crop, soil));
                }
            }

            return recommendations;
        }
        
        
        // Methiods to generate recommendations based on weather and soil data
        private IEnumerable<Recommendation> GenerateWeatherRecommendation(Crop crop, WeatherData weather, string city)
        {
            var recommendations = new List<Recommendation>();

            // Check weather conditions for rainfall and temperature
            if (weather.Rain < 10)
            {
                // Low rainfall recommendation
                recommendations.Add(new Recommendation
                {
                    CropId = crop.CropId,
                    RecommendationText = $"Low rainfall in {city}. Consider irrigating {crop.Name}.",
                    DateIssued = DateTime.Now,
                    Priority = "High",
                    WeatherSummary = $"Rain: {weather.Rain} mm, Temp: {weather.Temperature} °C"
                });
            }

            return recommendations;
        }

        private IEnumerable<Recommendation> GenerateSoilPhRecommendation(Crop crop, SoilData soil)
        {
            var recommendations = new List<Recommendation>();

            // Check soil pH and generate recommendations
            if (double.TryParse(soil?.SoilpH, out double pH))
            {
                // If Ph levels are low, suggest lime addition
                if (pH < 5.5)
                {
                    recommendations.Add(new Recommendation
                    {
                        CropId = crop.CropId,
                        RecommendationText = $"{crop.Name}: Soil is too acidic. Add lime.",
                        DateIssued = DateTime.Now,
                        Priority = "Medium",
                        WeatherSummary = $"pH: {pH}"
                    });
                }
                else if (pH > 7.5) //else if pH levels are high, suggest sulfur addition
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

            return recommendations;
        }

        private IEnumerable<Recommendation> GenerateSoilOrganicMatterRecommendation(Crop crop, SoilData soil)
        {
            var recommendations = new List<Recommendation>();

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

            return recommendations;
        }
    }
}
