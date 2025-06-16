using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

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

    public DbSet<VisualizationData> VisualizationData { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WaterQualityData>().OwnsOne(w => w.Location);
        modelBuilder.Entity<WaterQualityData>().OwnsOne(w => w.ContaminantsPpm);
        modelBuilder.Entity<EnvironmentData>().OwnsOne(w => w.Location);
        modelBuilder.Entity<AirQualityData>().OwnsOne(w => w.Location);
        modelBuilder.Entity<ElectricMeterData>().OwnsOne(w => w.Location);
        modelBuilder.Entity<ResidentCountData>().OwnsOne(w => w.Location);
        modelBuilder.Entity<TrafficData>().OwnsOne(w => w.Location);
        modelBuilder.Entity<VisualizationData>().OwnsOne(v => v.Volume);
        modelBuilder.Entity<VisualizationData>()
    .Property(v => v.Geometry)
    .HasColumnType("jsonb");


    }
}
