using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Application.Servives
{
    public interface IPatientService
    {
        public Task<Patient> GetPatient(int id);
        public Task<bool> AddPatient(Patient patient);
        public Task<Engine> GetEngine(int id);
    }
}
