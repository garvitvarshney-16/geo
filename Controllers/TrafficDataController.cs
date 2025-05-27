using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TrafficDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public TrafficDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<TrafficData>> GenerateData()
    {
        var count = await _context.TrafficData.CountAsync();
        var sensorId = $"{count + 1}_cctv_traffic_location";

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);
        var locationJson = $"{{\"lat\": {lat}, \"lon\": {lon}}}";

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
            VehicleCount = vehicleCount,
            AverageSpeedKmph = avgSpeed,
            TrafficCongestionLevel = congestionLevel,
            SignalViolations = signalViolations,
            AccidentsReported = accidentsReported,
            Timestamp = DateTime.UtcNow,
            Location = locationJson
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
    public async Task<ActionResult<IEnumerable<TrafficData>>> GetAll()
    {
        return await _context.TrafficData.ToListAsync();
    }

    [HttpPut("{sensorId}")]
    public async Task<IActionResult> Update(string sensorId, TrafficData data)
    {
        if (sensorId != data.SensorId) return BadRequest();

        _context.Entry(data).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TrafficData.Any(e => e.SensorId == sensorId))
                return NotFound();
            throw;
        }

        return NoContent();
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
