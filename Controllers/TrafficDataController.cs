using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class TrafficDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public TrafficDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
    /// <summary>
    /// Generates a new traffic data point with random values and stores it in the database.
    public async Task<ActionResult<TrafficData>> GenerateData()
    {
        var count = await _context.TrafficData.CountAsync();

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);

        // Round to whole numbers
        var newLat = Math.Round(lat);
        var newLon = Math.Round(lon);

        // Create JSON using the whole number values
        var locationJson = $"{{\"lat\": {newLat}, \"lon\": {newLon}}}";

        var sensorId = $"{count + 1}_cctv_traffic_{newLat}{newLon}";

        var vehicleCount = Random.Shared.Next(100, 1000);
        var avgSpeed = Math.Round(Random.Shared.NextDouble() * 60, 1);
        var congestionLevel = avgSpeed switch
        {
            < 15 => "High",
            < 30 => "Moderate",
            _ => "Low"
        };
        var signalViolations = Random.Shared.Next(0, 50);
        var accidentsReported = Random.Shared.Next(0, 10);

        var data = new TrafficData
        {
            SensorId = sensorId,
            Sensor_type = "traffic",
            Location = new Location
            {
                Lat = newLat,
                Lon = newLon
            },
            VehicleCount = vehicleCount,
            AverageSpeedKmph = avgSpeed,
            TrafficCongestionLevel = congestionLevel,
            SignalViolations = signalViolations,
            AccidentsReported = accidentsReported,
            Timestamp = DateTime.UtcNow,
        };

        _context.TrafficData.Add(data);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { sensorId = data.SensorId }, data);
    }

    [HttpGet("{sensorId}")]
    public async Task<ActionResult<TrafficData>> GetById(string sensorId)
    {
        var item = await _context.TrafficData.FindAsync(sensorId);
        if (item == null) return NotFound();
        return item;
    }

    [HttpGet("all")]
    public async Task<ActionResult<Dictionary<string, TrafficData>>> GetAll()
    {
        var dataList = await _context.TrafficData.ToListAsync();
        var indexedJson = dataList
        .Select((data, index) => new
        {
            Index = (index + 1).ToString(),
            Data = data
        })
            .ToDictionary(item => item.Index, item => item.Data);

        return Ok(indexedJson);
    }

    [HttpPut("{sensorId}")]
    public async Task<IActionResult> UpdateLocation(string sensorId, [FromBody] LocationUpdateModel update)
    {
        if (sensorId != update.SensorId)
            return BadRequest("Sensor ID in URL and body do not match.");

        var entity = await _context.TrafficData.FirstOrDefaultAsync(w => w.SensorId == sensorId);
        if (entity == null)
            return NotFound("Sensor not found.");

        // Update location
        entity.Location = new Location { Lat = update.Lat, Lon = update.Lon };;

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
    public async Task<IActionResult> Delete(string sensorId)
    {
        var data = await _context.TrafficData.FindAsync(sensorId);
        if (data == null) return NotFound();

        _context.TrafficData.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
