using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AirQualityDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public AirQualityDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<AirQualityData>> GenerateData()
    {
        var count = await _context.AirQualityData.CountAsync();
        var sensorId = $"{count + 1}_air_location";

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);
        var locationJson = $"{{\"lat\": {lat}, \"lon\": {lon}}}";

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
            AQI = aqi,
            Category = category,
            PM2_5 = Math.Round(Random.Shared.NextDouble() * 150, 1),
            PM10 = Math.Round(Random.Shared.NextDouble() * 200, 1),
            NO2 = Math.Round(Random.Shared.NextDouble() * 100, 1),
            CO = Math.Round(Random.Shared.NextDouble() * 5, 2),
            O3 = Math.Round(Random.Shared.NextDouble() * 120, 1),
            Timestamp = DateTime.UtcNow,
            Location = locationJson
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
    public async Task<ActionResult<IEnumerable<AirQualityData>>> GetAll()
    {
        return await _context.AirQualityData.ToListAsync();
    }

    [HttpPut("{sensorId}")]
    public async Task<IActionResult> Update(string sensorId, AirQualityData data)
    {
        if (sensorId != data.SensorId) return BadRequest();

        _context.Entry(data).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.AirQualityData.Any(e => e.SensorId == sensorId))
                return NotFound();
            throw;
        }

        return NoContent();
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
