using HaroldAdviser.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace HaroldAdviser.ViewModels
{
    public class Pipeline
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "status")]
        public PipelineStatus Status { get; set; }

        [JsonProperty(PropertyName = "commit_id")]
        public string CommitId { get; set; }
    }
}
