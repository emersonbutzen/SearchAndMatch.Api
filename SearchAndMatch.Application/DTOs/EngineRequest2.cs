using Newtonsoft.Json;

namespace SearchAndMatch.Application.DTOs
{
    public class EngineRequest2
    {
        [JsonProperty("patient")]
        public PatientRequest2 Patient { get; set; }
    }
}
