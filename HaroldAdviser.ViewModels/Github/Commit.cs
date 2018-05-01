using Newtonsoft.Json;
using System.Collections.Generic;

namespace HaroldAdviser.ViewModels.Github
{
    public class Commit
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "tree_id")]
        public string TreeId { get; set; }

        [JsonProperty(PropertyName = "distinct")]
        public bool Distinct { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "author")]
        public Credential Author { get; set; }

        [JsonProperty(PropertyName = "committer")]
        public Credential Commiter { get; set; }

        [JsonProperty(PropertyName = "added")]
        public List<string> Added { get; set; }

        [JsonProperty(PropertyName = "removed")]
        public List<string> Removed { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public List<string> Modified { get; set; }
    }
}
