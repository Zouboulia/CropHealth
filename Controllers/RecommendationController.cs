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

        [HttpGet("crop")]
        public async Task<ActionResult<List<Recommendation>>> GetRecommendations([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("City is required.");
            }

            var recommendations = await _monitoringService.GenerateRecommendationsAsync(city);

            return Ok(recommendations);
        }
    }
}