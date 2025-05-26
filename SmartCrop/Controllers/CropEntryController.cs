using Microsoft.AspNetCore.Mvc;
using SmartCrop.Data;
using SmartCrop.Shared.Models;

namespace SmartCrop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropEntryController : ControllerBase
    {
        private readonly SmartCropDbContext _dbContext;

        public CropEntryController(SmartCropDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCropWithField([FromBody] CropFieldRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CropName) || string.IsNullOrWhiteSpace(request.FieldLocation))
            {
                return BadRequest("Crop name and field location are required.");
            }

            var field = new Field
            {
                Location = request.FieldLocation,
                SoilType = request.SoilType ?? "Unknown",
                FarmerId = 1 // For now, use static farmer ID until login is implemented
            };

            _dbContext.Fields.Add(field);
            await _dbContext.SaveChangesAsync();

            var crop = new Crop
            {
                Name = request.CropName,
                Variety = request.Variety ?? "Generic",
                FieldId = field.FieldId
            };

            _dbContext.Crops.Add(crop);
            await _dbContext.SaveChangesAsync();

            return Ok(new { crop.CropId, field.FieldId });
        }
    }

    public class CropFieldRequest
    {
        public string CropName { get; set; }
        public string? Variety { get; set; }
        public string FieldLocation { get; set; }
        public string? SoilType { get; set; }
    }
}