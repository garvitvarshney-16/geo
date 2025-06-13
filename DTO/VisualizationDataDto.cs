namespace YourNamespace.Models
{
    public class VisualizationDataDto
    {
        public Guid SurveyId { get; set; }
        public Guid AnotationId { get; set; }
        public Guid ConstructionStageMasterId { get; set; }
        public VolumeDetails Volume { get; set; }
        public string Status { get; set; }

        public string Stage { get; set; }
    }
}
