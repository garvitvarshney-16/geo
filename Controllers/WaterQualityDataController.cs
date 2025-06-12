using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class WaterQualityDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public WaterQualityDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
    /// <summary>
    /// Generates a new water quality data point with random values and stores it in the database.
    public async Task<ActionResult<WaterQualityData>> GenerateData()
    {
        var count = await _context.WaterQualityData.CountAsync();

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);

        // Round to whole numbers
        var newLat = Math.Round(lat);
        var newLon = Math.Round(lon);

        // Create JSON using the whole number values
        var locationJson = $"{{\"lat\": {newLat}, \"lon\": {newLon}}}";

        var sensorId = $"{count + 1}_water_{newLat}{newLon}";

        var contaminants = new
        {
            lead = Math.Round(Random.Shared.NextDouble() * 0.05, 4),
            arsenic = Math.Round(Random.Shared.NextDouble() * 0.01, 4)
        };
        var contaminantsJson = JsonSerializer.Serialize(contaminants);

        var data = new WaterQualityData
        {
            SensorId = sensorId,
            Sensor_type = "water_quality",
            Location = locationJson,
            PhLevel = Math.Round(Random.Shared.NextDouble() * 2 + 6, 2), // 6.0 - 8.0
            TurbidityNTU = Math.Round(Random.Shared.NextDouble() * 5, 2),
            DissolvedOxygenMgPerL = Math.Round(Random.Shared.NextDouble() * 4 + 5, 2), // 5.0 - 9.0
            ContaminantsPpm = contaminantsJson,
            Timestamp = DateTime.UtcNow,
        };

        _context.WaterQualityData.Add(data);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { sensorId = data.SensorId }, data);
    }

    [HttpGet("{sensorId}")]
    /// <summary>
    /// Retrieves a water quality data point by its sensor ID.
    public async Task<ActionResult<WaterQualityData>> GetById(string sensorId)
    {
        var item = await _context.WaterQualityData.FindAsync(sensorId);
        if (item == null) return NotFound();
        return item;
    }

    [HttpGet("all")]
    /// <summary>
    /// Retrieves all water quality data points from the database in a numbered JSON object format.
    /// </summary>
    public async Task<ActionResult<Dictionary<string, WaterQualityData>>> GetAll()
    {
        var dataList = await _context.WaterQualityData.ToListAsync();

        var indexedJson = dataList
            .Select((data, index) => new { Index = (index + 1).ToString(), Data = data })
            .ToDictionary(item => item.Index, item => item.Data);

        return Ok(indexedJson);
    }


    [HttpPut("{sensorId}")]
    /// <summary>
    /// Updates the location and sensor ID of a water quality data point.
    public async Task<IActionResult> UpdateLocation(string sensorId, [FromBody] LocationUpdateModel update)
    {
        if (sensorId != update.SensorId)
            return BadRequest("Sensor ID in URL and body do not match.");

        var entity = await _context.WaterQualityData.FirstOrDefaultAsync(w => w.SensorId == sensorId);
        if (entity == null)
            return NotFound("Sensor not found.");

        // Update location
        entity.Location = JsonSerializer.Serialize(new { lat = update.Lat, lon = update.Lon });

        // Build new SensorId based on lat and lon
        // string prefix = sensorId.Substring(0, sensorId.LastIndexOf('_') + 1); // "27_water_"
        // string newSensorId = $"{prefix}{(int)update.Lat}{(int)update.Lon}";
        // entity.SensorId = newSensorId;

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

    [HttpDelete("{sensorId}")]
    /// <summary>
    /// Deletes a water quality data point by its sensor ID.
    public async Task<IActionResult> Delete(string sensorId)
    {
        var data = await _context.WaterQualityData.FindAsync(sensorId);
        if (data == null) return NotFound();

        _context.WaterQualityData.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
