using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Domain.Interfaces
{
    public interface IEngineRepository
    {
        Task<bool> AddAsync(Engine entity);
        Task<bool> UpdateAsync(Engine entity);
        Task<bool> DeleteAsync(int id);
        Task<Engine> FindAsync(int id);
        Task<IEnumerable<Engine>> FindAllAsync();
    }
}
