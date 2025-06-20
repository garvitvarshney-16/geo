using System;
using System.ComponentModel.DataAnnotations;
public class ResidentCountData
{
    [Key]
    public string SensorId { get; set; }

    public string Sensor_type { get; set; }
    public string ResidentialBlock { get; set; }
    public int NumberOfResidents { get; set; }
    public int NumberOfHouseholds { get; set; }
    public DateTime Timestamp { get; set; }
    public Location Location { get; set; }
}
