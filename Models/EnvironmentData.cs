using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EnvironmentData
{
    [Key]
    public string SensorId { get; set; }

    public string Sensor_type { get; set; }

    public double TemperatureCelsius { get; set; }
    public double HumidityPercent { get; set; }
    public double UvIndex { get; set; }
    public double NoiseLevelDb { get; set; }
    public DateTime Timestamp { get; set; }

    // Store JSON string
    public string Location { get; set; } = "{}";
}
