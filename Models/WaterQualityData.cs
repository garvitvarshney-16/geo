using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WaterQualityData
{
    [Key]
    public string SensorId { get; set; }

    public string Sensor_type { get; set; }

    public Location Location { get; set; }

    public double PhLevel { get; set; }

    public double TurbidityNTU { get; set; }

    public double DissolvedOxygenMgPerL { get; set; }

    // JSON string for contaminants
    public Contaminants ContaminantsPpm { get; set; }

    public DateTime Timestamp { get; set; }
}


public class Location
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}

public class Contaminants
{
    public double Lead { get; set; }
    public double Arsenic { get; set; }
}




// dotnet ef migrations add InitialCreate
// dotnet ef database update