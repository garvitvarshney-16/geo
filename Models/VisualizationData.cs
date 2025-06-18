using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace YourNamespace.Models
{
    public class VisualizationData
    {
        [Key]
        public int Id { get; set; }

        public Guid SurveyId { get; set; }
        public Guid AnotationId { get; set; }

        public Guid ConstructionStageMasterId { get; set; }

        public VolumeDetails Volume { get; set; }

        public string Status { get; set; }

        public string Stage { get; set; }

        public decimal Chainage_to { get; set; }

        public decimal Chainage_from { get; set; }

        public decimal mean_elev { get; set; }

        public JsonDocument? Geometry { get; set; } // ✅ Must be nullable with `?`


        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

public class VolumeDetails
{
    public double Cut { get; set; }
    public double Fill { get; set; }
    public double Net { get; set; }
    public double Total { get; set; }
}