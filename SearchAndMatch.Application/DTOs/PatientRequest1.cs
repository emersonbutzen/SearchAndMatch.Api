using Newtonsoft.Json;

namespace SearchAndMatch.Application.DTOs
{
    public class PatientRequest1 : IPatientRequest
    {
        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty("diseaseType")]
        public string DiseaseType { get; set; }
        [JsonProperty("forename")]
        public string Forename { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
    }
}
