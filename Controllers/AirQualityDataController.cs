using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class AirQualityDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public AirQualityDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<AirQualityData>> GenerateData()
    {
        var count = await _context.AirQualityData.CountAsync();


        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);
        // Round to whole numbers
        var newLat = Math.Round(lat);
        var newLon = Math.Round(lon);

        // Create JSON using the whole number values
        var locationJson = $"{{\"lat\": {newLat}, \"lon\": {newLon}}}";

        var sensorId = $"{count + 1}_air_{newLat}{newLon}";

        var aqi = Random.Shared.Next(0, 300);
        string category = aqi switch
        {
            <= 50 => "Good",
            <= 100 => "Moderate",
            <= 150 => "Unhealthy for Sensitive Groups",
            <= 200 => "Unhealthy",
            <= 300 => "Very Unhealthy",
            _ => "Hazardous"
        };

        var data = new AirQualityData
        {
            SensorId = sensorId,
            Sensor_type = "air_quality",
            Location = locationJson,
            AQI = aqi,
            Category = category,
            PM2_5 = Math.Round(Random.Shared.NextDouble() * 150, 1),
            PM10 = Math.Round(Random.Shared.NextDouble() * 200, 1),
            NO2 = Math.Round(Random.Shared.NextDouble() * 100, 1),
            CO = Math.Round(Random.Shared.NextDouble() * 5, 2),
            O3 = Math.Round(Random.Shared.NextDouble() * 120, 1),
            Timestamp = DateTime.UtcNow,
        };

        _context.AirQualityData.Add(data);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { sensorId = data.SensorId }, data);
    }

    [HttpGet("{sensorId}")]
    public async Task<ActionResult<AirQualityData>> GetById(string sensorId)
    {
        var item = await _context.AirQualityData.FindAsync(sensorId);
        if (item == null) return NotFound();
        return item;
    }

    [HttpGet("all")]
    public async Task<ActionResult<Dictionary<string, AirQualityData>>> GetAll()
    {
        var dataList = await _context.AirQualityData.ToListAsync();
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

        var entity = await _context.ElectricMeterData.FirstOrDefaultAsync(w => w.SensorId == sensorId);
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

    [HttpDelete("{sensorId}")]
    public async Task<IActionResult> Delete(string sensorId)
    {
        var data = await _context.AirQualityData.FindAsync(sensorId);
        if (data == null) return NotFound();

        _context.AirQualityData.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
