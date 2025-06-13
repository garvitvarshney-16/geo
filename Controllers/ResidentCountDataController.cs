using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class ResidentCountDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public ResidentCountDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<ResidentCountData>> GenerateData()
    {
        var count = await _context.ResidentCountData.CountAsync();


        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);

        // Round to whole numbers
        var newLat = Math.Round(lat);
        var newLon = Math.Round(lon);

        // Create JSON using the whole number values
        var locationJson = $"{{\"lat\": {newLat}, \"lon\": {newLon}}}";

        var sensorId = $"{count + 1}_resident_{newLat}{newLon}";

        var block = $"Block {(char)('A' + Random.Shared.Next(0, 10))}";
        var residents = Random.Shared.Next(50, 500);
        var households = Random.Shared.Next(10, 100);

        var data = new ResidentCountData
        {
            SensorId = sensorId,
            Sensor_type = "resident",
            Location = new Location
            {
                Lat = newLat,
                Lon = newLon
            },
            ResidentialBlock = block,
            NumberOfResidents = residents,
            NumberOfHouseholds = households,
            Timestamp = DateTime.UtcNow,
        };

        _context.ResidentCountData.Add(data);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { sensorId = data.SensorId }, data);
    }

    [HttpGet("{sensorId}")]
    public async Task<ActionResult<ResidentCountData>> GetById(string sensorId)
    {
        var item = await _context.ResidentCountData.FindAsync(sensorId);
        if (item == null) return NotFound();
        return item;
    }

    [HttpGet("all")]
    public async Task<ActionResult<Dictionary<string, ResidentCountData>>> GetAll()
    {
        var dataList = await _context.ResidentCountData.ToListAsync();
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

        var entity = await _context.ResidentCountData.FirstOrDefaultAsync(w => w.SensorId == sensorId);
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
        var data = await _context.ResidentCountData.FindAsync(sensorId);
        if (data == null) return NotFound();

        _context.ResidentCountData.Remove(data);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
