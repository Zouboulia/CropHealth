using Microsoft.AspNetCore.Mvc;
using SmartCrop.Data;
using SmartCrop.Shared.Models;

namespace SmartCrop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly SmartCropDbContext _dbContext;

        public SeedController(SmartCropDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult SeedData()
        {
            // Avoid seeding duplicate data
            if (_dbContext.Crops.Any())
                return Ok("Data already exists.");

            // Create a Farmer
            var farmer = new Farmer { Name = "John Doe" };
            _dbContext.Farmers.Add(farmer);
            _dbContext.SaveChanges();

            // Create a Field
            var field = new Field
            {
                Location = "Test Field Location",
                SoilType = "Loamy",
                FarmerId = farmer.FarmerId
            };
            _dbContext.Fields.Add(field);
            _dbContext.SaveChanges();

            // Create a Crop
            var crop = new Crop
            {
                Name = "Wheat",
                Variety = "Golden",
                FieldId = field.FieldId
            };
            _dbContext.Crops.Add(crop);
            _dbContext.SaveChanges();

            // Add Soil Data
            var soil = new SoilData
            {
                FieldId = field.FieldId,
                SoilMoisture = "Medium",
                SoilpH = "5.2",
                SoilOrganicMatter = "3.1",
                SoilType = "Loamy"
            };
            _dbContext.SoilData.Add(soil);
            _dbContext.SaveChanges();

            return Ok("Seed data created.");
        }
    }
}