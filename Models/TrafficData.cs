using System;
using System.ComponentModel.DataAnnotations;

public class TrafficData
{
    [Key]
    public string SensorId { get; set; }
    public int VehicleCount { get; set; }
    public double AverageSpeedKmph { get; set; }
    public string TrafficCongestionLevel { get; set; }
    public int SignalViolations { get; set; }
    public int AccidentsReported { get; set; }
    public DateTime Timestamp { get; set; }
    public string Location { get; set; }
}
