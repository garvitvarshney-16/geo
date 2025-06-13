using System;
using System.ComponentModel.DataAnnotations;

public class ElectricMeterData
{
    [Key]
    public string SensorId { get; set; }

    public string Sensor_type { get; set; }

    public string HouseholdId { get; set; }
    public string HouseArea { get; set; }
    public double ConsumptionKWh { get; set; }
    public string MeterStatus { get; set; }
    public string BillingCycle { get; set; }

    public DateTime Timestamp { get; set; }
    public Location Location { get; set; }
}
