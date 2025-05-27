using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ElectricMeterDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public ElectricMeterDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ElectricMeterData>> GenerateData()
    {
        var count = await _context.ElectricMeterData.CountAsync();
        var sensorId = $"{count + 1}_elec_meter_location";

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);
        var locationJson = $"{{\"lat\": {lat}, \"lon\": {lon}}}";

        var data = new ElectricMeterData
        {
            SensorId = sensorId,
            HouseholdId = $"H{Random.Shared.Next(1000, 9999)}",
            HouseArea = $"{Random.Shared.Next(30, 150)}sqm",
            ConsumptionKWh = Math.Round(Random.Shared.NextDouble() * 100, 2),
            MeterStatus = Random.Shared.Next(0, 2) == 0 ? "active" : "inactive",
            BillingCycle = DateTime.UtcNow.ToString("yyyy-MM"),
            Timestamp = DateTime.UtcNow,
            Location = locationJson
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
    public async Task<ActionResult<IEnumerable<ElectricMeterData>>> GetAll()
    {
        return await _context.ElectricMeterData.ToListAsync();
    }

    [HttpPut("{sensorId}")]
    public async Task<IActionResult> Update(string sensorId, ElectricMeterData data)
    {
        if (sensorId != data.SensorId) return BadRequest();

        _context.Entry(data).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ElectricMeterData.Any(e => e.SensorId == sensorId))
                return NotFound();
            throw;
        }

        return NoContent();
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
