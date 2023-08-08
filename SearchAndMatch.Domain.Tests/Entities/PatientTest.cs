using FluentAssertions;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Helper;

namespace SearchAndMatch.Domain.Tests.Entities
{
    [Trait("Category", "Unit")]
    public class PatientTest
    {
        [Theory, AutoMoq]
        public void CreateNewPatient_ShouldSetInformations(Patient patientInformations)
        {

            var engine = new Patient()
            {
                Id = patientInformations.Id,
                FirstName = patientInformations.FirstName,
                LastName = patientInformations.LastName,
                DateOfBirth = patientInformations.DateOfBirth,
                DiseaseType = patientInformations.DiseaseType
            };

            engine.Id.Should().Be(patientInformations.Id);
            engine.FirstName.Should().Be(patientInformations.FirstName);
            engine.LastName.Should().Be(patientInformations.LastName);
            engine.DateOfBirth.Should().Be(patientInformations.DateOfBirth);
            engine.DiseaseType.Should().Be(patientInformations.DiseaseType);
        }
    }
}