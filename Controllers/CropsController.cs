using Microsoft.AspNetCore.Mvc;
using SmartCrop.Shared.Models;
using Microsoft.EntityFrameworkCore;
using SmartCrop.Data;


namespace SmartCrop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropsController : ControllerBase
    {
        private readonly SmartCropDbContext _db;

        public CropsController(SmartCropDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllCrops()
        {
            var crops = await _db.Crops
                .Include(c => c.Field)
                .ThenInclude(f => f.SoilData)
                .Select(c => new
                {
                    c.CropId,
                    c.Name,
                    c.Variety,
                    FieldLocation = c.Field.Location,
                    SoilType = c.Field.SoilType,
                    SoilMoisture = c.Field.SoilData.SoilMoisture,
                    SoilpH = c.Field.SoilData.SoilpH,
                    SoilOrganicMatter = c.Field.SoilData.SoilOrganicMatter
                })
                .ToListAsync();

            return Ok(crops);
        }
    }
}