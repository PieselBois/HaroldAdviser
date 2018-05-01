using HaroldAdviser.ViewModels.Github;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace HaroldAdviser.ViewModels
{
    public class GithubWebhook : IWebhook
    {
        [JsonProperty(PropertyName = "ref")]
        public string Ref { get; set; }

        [JsonProperty(PropertyName = "before")]
        public string Before { get; set; }

        [JsonProperty(PropertyName = "after")]
        public string After { get; set; }

        [JsonProperty(PropertyName = "created")]
        public bool Created { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }

        [JsonProperty(PropertyName = "forced")]
        public bool Forced { get; set; }

        [JsonProperty(PropertyName = "base_ref")]
        public string BaseRef { get; set; }

        [JsonProperty(PropertyName = "compare")]
        public string Compare { get; set; }

        [JsonProperty(PropertyName = "commits")]
        public List<Commit> Commits { get; set; }

        [JsonProperty(PropertyName = "head_commit")]
        public Commit HeadCommit { get; set; }

        [JsonProperty(PropertyName = "repository")]
        public Github.Repository Repository { get; set; }

        [JsonProperty(PropertyName = "pusher")]
        public Credential Pusher { get; set; }

        [JsonProperty(PropertyName = "sender")]
        public Sender Sender { get; set; }

        public string CloneUrl => Commits.Last().Url;

        public string HtmlUrl => Repository.HtmlUrl;
    }
}