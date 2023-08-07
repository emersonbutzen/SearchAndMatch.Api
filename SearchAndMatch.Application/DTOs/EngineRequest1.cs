using Newtonsoft.Json;

namespace SearchAndMatch.Application.DTOs
{
    public class EngineRequest1
    {
        [JsonProperty("patient")]
        public PatientRequest1 Patient { get; set; }
    }
}
