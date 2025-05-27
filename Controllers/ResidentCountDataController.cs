using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ResidentCountDataController : ControllerBase
{
    private readonly AppDbContext _context;

    public ResidentCountDataController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ResidentCountData>> GenerateData()
    {
        var count = await _context.ResidentCountData.CountAsync();
        var sensorId = $"{count + 1}_resident_block";

        var lat = Math.Round(Random.Shared.NextDouble() * 90, 6);
        var lon = Math.Round(Random.Shared.NextDouble() * 180, 6);
        var locationJson = $"{{\"lat\": {lat}, \"lon\": {lon}}}";

        var block = $"Block {(char)('A' + Random.Shared.Next(0, 10))}";
        var residents = Random.Shared.Next(50, 500);
        var households = Random.Shared.Next(10, 100);

        var data = new ResidentCountData
        {
            SensorId = sensorId,
            ResidentialBlock = block,
            NumberOfResidents = residents,
            NumberOfHouseholds = households,
            Timestamp = DateTime.UtcNow,
            Location = locationJson
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
    public async Task<ActionResult<IEnumerable<ResidentCountData>>> GetAll()
    {
        return await _context.ResidentCountData.ToListAsync();
    }

    [HttpPut("{sensorId}")]
    public async Task<IActionResult> Update(string sensorId, ResidentCountData data)
    {
        if (sensorId != data.SensorId) return BadRequest();

        _context.Entry(data).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ResidentCountData.Any(e => e.SensorId == sensorId))
                return NotFound();
            throw;
        }

        return NoContent();
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
