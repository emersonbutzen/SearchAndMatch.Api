using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<bool> AddAsync(Patient entity);
        Task<bool> UpdateAsync(Patient entity);
        Task<bool> DeleteAsync(int id);
        Task<Patient> FindAsync(int id);
        Task<IEnumerable<Patient>> FindAllAsync();
    }
}
