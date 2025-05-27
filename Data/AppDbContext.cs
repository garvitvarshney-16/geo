using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<EnvironmentData> EnvironmentData { get; set; }
    public DbSet<WaterQualityData> WaterQualityData { get; set; }

    public DbSet<AirQualityData> AirQualityData { get; set; }

    public DbSet<ElectricMeterData> ElectricMeterData { get; set; }

    public DbSet<ResidentCountData> ResidentCountData { get; set; }

    public DbSet<TrafficData> TrafficData { get; set; }


}
