using System;
using System.ComponentModel.DataAnnotations;

public class AirQualityData
{
    [Key]
    public string SensorId { get; set; }

    public int AQI { get; set; }

    public string Category { get; set; }

    public double PM2_5 { get; set; }

    public double PM10 { get; set; }

    public double NO2 { get; set; }

    public double CO { get; set; }

    public double O3 { get; set; }

    public DateTime Timestamp { get; set; }

    public string Location { get; set; }
}
