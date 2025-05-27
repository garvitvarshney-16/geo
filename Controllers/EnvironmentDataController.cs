using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class EnvironmentDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public EnvironmentDataController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/EnvironmentData — Generate and store a new data point
    [HttpPost("generate")]
    public async Task<ActionResult<EnvironmentData>> GenerateData()
    {
        // Generate a unique SensorId based on count
        var sensorCount = await _context.EnvironmentData.CountAsync();

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);
        // Round to whole numbers
        var newLat = Math.Round(lat);
        var newLon = Math.Round(lon);

        // Create JSON using the whole number values
        var locationJson = $"{{\"lat\": {newLat}, \"lon\": {newLon}}}";

        var sensorId = $"{sensorCount + 1}_env_{newLat}{newLon}";

        var data = new EnvironmentData
        {
            SensorId = sensorId,
            Sensor_type = "environment",
            Location = locationJson,
            TemperatureCelsius = Random.Shared.NextDouble() * 15 + 20,
            HumidityPercent = Random.Shared.Next(30, 70),
            UvIndex = Math.Round(Random.Shared.NextDouble() * 10, 1),
            NoiseLevelDb = Random.Shared.Next(40, 90),
            Timestamp = DateTime.UtcNow,
        };

        _context.EnvironmentData.Add(data);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { sensorId = data.SensorId }, data);
    }

    // GET by ID
    [HttpGet("{sensorId}")]
    public async Task<ActionResult<EnvironmentData>> GetById(string sensorId)
    {
        var item = await _context.EnvironmentData.FindAsync(sensorId);
        if (item == null) return NotFound();

        return item;
    }

    // GET all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<EnvironmentData>>> GetAll()
    {
        return await _context.EnvironmentData.ToListAsync();
    }

    // PUT (update)
    [HttpPut("{sensorId}")]
    public async Task<IActionResult> Update(string sensorId, [FromBody] LocationUpdateModel update)
    {
        if (sensorId != update.SensorId)
            return BadRequest("Sensor ID in URL and body do not match.");

        var entity = await _context.EnvironmentData.FirstOrDefaultAsync(w => w.SensorId == sensorId);
        if (entity == null)
            return NotFound("Sensor not found.");

        // Update location
        entity.Location = JsonSerializer.Serialize(new { lat = update.Lat, lon = update.Lon });

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, "A concurrency error occurred while updating.");
        }

        return Ok(new { message = "Location updated successfully" });
    }

    // DELETE
    [HttpDelete("{sensorId}")]
    public async Task<IActionResult> Delete(string sensorId)
    {
        var data = await _context.EnvironmentData.FindAsync(sensorId);
        if (data == null) return NotFound();

        _context.EnvironmentData.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
