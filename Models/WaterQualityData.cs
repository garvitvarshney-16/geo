using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WaterQualityData
{
    [Key]
    public string SensorId { get; set; }

    public string Sensor_type { get; set; }

    public string Location { get; set; }

    public double PhLevel { get; set; }

    public double TurbidityNTU { get; set; }

    public double DissolvedOxygenMgPerL { get; set; }

    // JSON string for contaminants
    public string ContaminantsPpm { get; set; }

    public DateTime Timestamp { get; set; }
}
