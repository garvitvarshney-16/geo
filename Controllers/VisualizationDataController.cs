using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisualizationDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisualizationDataController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("volume")]
        public async Task<IActionResult> PostVolumeData([FromBody] VisualizationDataDto input)
        {
            // if (input == null || input.Volume == null)
            //     return BadRequest("Invalid input data.");

            var newData = new VisualizationData
            {
                SurveyId = input.SurveyId,
                AnotationId = input.AnotationId,
                ConstructionStageMasterId = input.ConstructionStageMasterId,
                Volume = input.Volume,
                Status = input.Status,
                Chainage_to = input.Chainage_to,
                Chainage_from = input.Chainage_from,
                mean_elev = input.mean_elev,
                Geometry = input.GeometryJson,
                Stage = input.Stage
            };

            _context.VisualizationData.Add(newData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newData.Id }, newData);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisualizationData>> GetById(int id)
        {
            var item = await _context.VisualizationData.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("ano/{anotationId}")]
        public async Task<ActionResult<VisualizationData>> GetByAnotationId(Guid anotationId)
        {
            var item = await _context.VisualizationData
                .FirstOrDefaultAsync(v => v.AnotationId == anotationId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<VisualizationData>>> GetAll()
        {
            return Ok(await _context.VisualizationData.ToListAsync());
        }
    }
}
