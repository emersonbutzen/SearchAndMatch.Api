using Newtonsoft.Json;

namespace SearchAndMatch.Application.DTOs
{
    public class EndpointResponse
    {
        [JsonProperty("about")]
        public string About { get; set; }
        [JsonProperty("Timestamp")]
        public string Timestamp { get; set; }
    }
}
