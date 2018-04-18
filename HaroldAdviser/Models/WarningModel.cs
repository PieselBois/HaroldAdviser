using Newtonsoft.Json;

namespace HaroldAdviser.Models
{
    public class WarningModel
    {
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "file")]
        public string File { get; set; }

        [JsonProperty(PropertyName = "lines")]
        public string Lines { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
