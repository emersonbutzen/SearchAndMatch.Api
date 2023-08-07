using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Domain.Interfaces;

namespace SearchAndMatch.Application.Servives
{
    public class PatientService : IPatientService
    {
        private IPatientRepository _patientRepository;
        private IEngineRepository _engineRepository;
        public PatientService(IPatientRepository patientRepository, IEngineRepository engineRepository)
        {
            _patientRepository = patientRepository;
            _engineRepository = engineRepository;
        }
        public async Task<bool> AddPatient(Patient patient)
        {
            return await _patientRepository.AddAsync(patient);
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await _patientRepository.FindAsync(id);
        }

        public async Task<Engine> GetEngine(int id)
        {
            return await _engineRepository.FindAsync(id);
        }
    }
}
