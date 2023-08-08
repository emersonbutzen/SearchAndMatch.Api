using AutoFixture.Xunit2;
using FluentAssertions;
using SearchAndMatch.DAL.Context;
using SearchAndMatch.DAL.Repositories;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Helper;

namespace SearchAndMatch.DAL.Tests.Repositories
{
    [Trait("Category", "Unit")]
    public class PatientRepositoryTest
    {
        [Theory, AutoMoq]
        public async Task Given_AnPatient_ShouldAdd(
           PatientRepository sut,
           Patient Patient)
        {
            var result = await sut.AddAsync(Patient);

            result.Should().BeTrue();
        }

        [Theory, AutoMoq]
        public async Task Given_AnExistentPatient_ShouldUpdate(
            PatientRepository sut,
            Patient Patient)
        {
            await sut.AddAsync(Patient);

            Patient.FirstName = "updated";
            var result = await sut.UpdateAsync(Patient);

            var getById = await sut.FindAsync(Patient.Id);

            result.Should().BeTrue();
            getById.FirstName.Should().BeEquivalentTo(Patient.FirstName);
        }

        [Theory, AutoMoq]
        public async Task Given_AnExistentPatient_ShouldDelete(
            PatientRepository sut,
            Patient Patient)
        {
            await sut.AddAsync(Patient);

            var delete = await sut.DeleteAsync(Patient.Id);
            delete.Should().BeTrue();

            var getById = await sut.FindAsync(Patient.Id);
            getById.Should().BeNull();
        }

        [Theory, AutoMoq]
        public void FindAsync_ShouldReturnCorrectPatient(
            [Frozen] SearchAndMatchContext _searchAndMatchContext,
            PatientRepository sut,
            List<Patient> Patients)
        {
            // Arrange
            _searchAndMatchContext.Patients.AddRange(Patients);
            _searchAndMatchContext.SaveChanges();
            Patient Patient = Patients[0];

            // Act
            var result = sut.FindAsync(Patient.Id);

            // Assert
            result.Result.Should().BeOfType<Patient>();
            result.Result.Should().BeEquivalentTo(Patient);
        }

        [Theory, AutoMoq]
        public async Task Given_AnExistentPatientId_ShouldReturnPatient(
            PatientRepository sut,
            Patient Patient)
        {
            await sut.AddAsync(Patient);

            var result = sut.FindAsync(Patient.Id);

            result.Result.Should().BeEquivalentTo(Patient);
        }

        [Theory, AutoMoq]
        public void ListAsync_ShouldReturnAllPatients(
            [Frozen] SearchAndMatchContext _searchAndMatchContext,
        PatientRepository sut,
            List<Patient> Patients)
        {
            // Arrange
            _searchAndMatchContext.Patients.AddRange(Patients);
            _searchAndMatchContext.SaveChanges();

            // Act
            var result = sut.FindAllAsync();

            // Assert
            result.Result.Should().BeEquivalentTo(Patients);
        }
    }
}