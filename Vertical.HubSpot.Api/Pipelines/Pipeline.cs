namespace Vertical.HubSpot.Api.Pipelines {
    public class Pipeline {
        public string PipelineId { get; set; }

        public PipelineType ObjectType { get; set; }

        public string Label { get; set; }

        public int DisplayOrder { get; set; }

        public bool Active { get; set; }


    }
}