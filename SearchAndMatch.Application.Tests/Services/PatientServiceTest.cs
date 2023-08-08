using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SearchAndMatch.Application.Servives;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Domain.Interfaces;
using SearchAndMatch.Helper;

namespace SearchAndMatch.Application.Tests.Services
{
    public class PatientServiceTest
    {
        [Theory, AutoMoq]
        public async Task AddPatient_ShouldAdded(
            [Frozen] Mock<IPatientRepository> patientRepositoryMock,
            Patient patientMock,
            PatientService sut
            )
        {
            // Arrange
            patientRepositoryMock.Setup(m => m.AddAsync(patientMock)).ReturnsAsync(true);

            // Act
            await sut.AddPatient(patientMock);

            // Assert
            patientRepositoryMock.Verify(m => m.AddAsync(patientMock), Times.Once);
        }

        [Theory, AutoMoq]
        public async Task GetPatient_ShouldReturn(
            [Frozen] Mock<IPatientRepository> patientRepositoryMock,
            Patient patientMock,
            PatientService sut
        )
        {
            // Arrange
            patientRepositoryMock.Setup(m => m.FindAsync(patientMock.Id)).ReturnsAsync(patientMock);

            // Act
            var result = await sut.GetPatient(patientMock.Id);

            // Assert
            patientRepositoryMock.Verify(m => m.FindAsync(patientMock.Id), Times.Once);
            result.Should().Be(patientMock);
        }

        [Theory, AutoMoq]
        public async Task GetEngine_ShouldReturn(
            [Frozen] Mock<IEngineRepository> engineRepositoryMock,
            Engine engineMock,
            PatientService sut
            )
        {
            // Arrange
            engineRepositoryMock.Setup(m => m.FindAsync(engineMock.Id)).ReturnsAsync(engineMock);

            // Act
            var result = await sut.GetEngine(engineMock.Id);

            // Assert
            engineRepositoryMock.Verify(m => m.FindAsync(engineMock.Id), Times.Once);
            result.Should().Be(engineMock);
        }
    }
}
