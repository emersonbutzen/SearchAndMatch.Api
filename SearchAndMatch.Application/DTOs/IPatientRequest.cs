namespace SearchAndMatch.Application.DTOs
{
    public interface IPatientRequest
    {
        public string DateOfBirth { get; set; }

        public string DiseaseType { get; set; }
    }
}
