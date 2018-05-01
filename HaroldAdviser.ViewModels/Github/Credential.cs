using Newtonsoft.Json;

namespace HaroldAdviser.ViewModels.Github
{
    public class Credential
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
    }
}
