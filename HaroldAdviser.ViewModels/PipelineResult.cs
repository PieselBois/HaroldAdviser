using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

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