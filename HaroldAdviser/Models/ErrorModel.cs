using Newtonsoft.Json;
using System.Collections.Generic;

namespace HaroldAdviser.Models
{
    public class ErrorModel
    {
        [JsonProperty(PropertyName = "error")]
        public IList<ErrorInfo> Info { get; set; }
    }
}
