using Microsoft.AspNetCore.Mvc;
using SmartCrop.Client.Pages;
using SmartCrop.Data;
using SmartCrop.Shared.Models;

namespace SmartCrop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataEntryController : ControllerBase
    {
        private readonly SmartCropDbContext _db;

        public DataEntryController(SmartCropDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEntry([FromBody] EntryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // For now we assign a dummy FarmerId (e.g., 1)
            var field = new Field
            {
                Location = model.FieldLocation,
                SoilType = model.SoilType,
                FarmerId = 1 // Adjust when authentication or farmer logic is added
            };

            _db.Fields.Add(field);
            await _db.SaveChangesAsync();

            var crop = new Crop
            {
                Name = model.CropName,
                Variety = model.CropVariety,
                FieldId = field.FieldId
            };
            _db.Crops.Add(crop);
            await _db.SaveChangesAsync();

            var soil = new SoilData
            {
                FieldId = field.FieldId,
                SoilType = model.SoilType,
                SoilMoisture = model.SoilMoisture,
                SoilpH = model.SoilpH,
                SoilOrganicMatter = model.SoilOrganicMatter
            };
            _db.SoilData.Add(soil);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Data saved successfully" });
        }
    }
}