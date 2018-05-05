using Newtonsoft.Json;

namespace HaroldAdviser.ViewModels.Github
{
    public class Repository
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public Credential Owner { get; set; }

        [JsonProperty(PropertyName = "private")]
        public bool Private { get; set; }

        [JsonProperty(PropertyName = "html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "fork")]
        public bool Fork { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "pushed_at")]
        public int PushedAt { get; set; }

        [JsonProperty(PropertyName = "git_url")]
        public string GitUrl { get; set; }

        [JsonProperty(PropertyName = "ssh_url")]
        public string SshUrl { get; set; }

        [JsonProperty(PropertyName = "clone_url")]
        public string CloneUrl { get; set; }

        [JsonProperty(PropertyName = "svn_url")]
        public string SvnUrl { get; set; }

        [JsonProperty(PropertyName = "default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "master_branch")]
        public string MasterBranch { get; set; }
    }
}
