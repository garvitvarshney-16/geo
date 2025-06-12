using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class ElectricMeterDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public ElectricMeterDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<ElectricMeterData>> GenerateData()
    {
        var count = await _context.ElectricMeterData.CountAsync();

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);

        // Round to whole numbers
        var newLat = Math.Round(lat);
        var newLon = Math.Round(lon);

        // Create JSON using the whole number values
        var locationJson = $"{{\"lat\": {newLat}, \"lon\": {newLon}}}";

        var sensorId = $"{count + 1}_elec_meter_{newLat}{newLon}";

        var data = new ElectricMeterData
        {
            SensorId = sensorId,
            Sensor_type = "electric_meter",
            Location = locationJson,
            HouseholdId = $"H{Random.Shared.Next(1000, 9999)}",
            HouseArea = $"{Random.Shared.Next(30, 150)}sqm",
            ConsumptionKWh = Math.Round(Random.Shared.NextDouble() * 100, 2),
            MeterStatus = Random.Shared.Next(0, 2) == 0 ? "active" : "inactive",
            BillingCycle = DateTime.UtcNow.ToString("yyyy-MM"),
            Timestamp = DateTime.UtcNow,
        };

        _context.ElectricMeterData.Add(data);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { sensorId = data.SensorId }, data);
    }

    [HttpGet("{sensorId}")]
    public async Task<ActionResult<ElectricMeterData>> GetById(string sensorId)
    {
        var item = await _context.ElectricMeterData.FindAsync(sensorId);
        if (item == null) return NotFound();
        return item;
    }

    [HttpGet("all")]
    public async Task<ActionResult<Dictionary<string, ElectricMeterData>>> GetAll()
    {
        var dataList = await _context.ElectricMeterData.ToListAsync();
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
        var data = await _context.ElectricMeterData.FindAsync(sensorId);
        if (data == null) return NotFound();

        _context.ElectricMeterData.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
