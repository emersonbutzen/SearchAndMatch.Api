using Newtonsoft.Json;

namespace SearchAndMatch.Application.DTOs
{
    public class PatientRequest2: IPatientRequest
    {
        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }
        [JsonProperty("diseaseType")]
        public string DiseaseType { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
