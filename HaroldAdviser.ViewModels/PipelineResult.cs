using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HaroldAdviser.ViewModels
{
    public class PipelineResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public IList<string> Errors { get; set; }

        [JsonProperty(PropertyName = "warnings")]
        public IList<WarningModel> Warnings { get; set; }
    }
}