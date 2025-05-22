using Microsoft.AspNetCore.Mvc;
using SmartCrop.Services;
using SmartCrop.Shared.Models;

namespace SmartCrop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly MonitoringService _monitoringService;

        public RecommendationController(MonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        [HttpGet("crop/{cropId}")]
        public async Task<ActionResult<List<Recommendation>>> GetRecommendations(int cropId, [FromQuery] string city)
        {
            var recommendations = await _monitoringService.GenerateRecommendationsAsync(cropId, city);
            return Ok(recommendations);
        }
    }
}

