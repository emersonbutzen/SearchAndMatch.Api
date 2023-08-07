using Microsoft.EntityFrameworkCore;
using SearchAndMatch.DAL.Context;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Domain.Interfaces;

namespace SearchAndMatch.DAL.Repositories
{
    public class EngineRepository : IEngineRepository
    {
        protected SearchAndMatchContext _context;
        public EngineRepository(SearchAndMatchContext context) { 
            _context = context;
        }

        private bool EngineExists(int id)
        {
            return (_context.Engines?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<bool> AddAsync(Engine entity)
        {
            _context.Engines.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Engine entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineExists(entity.Id))
                {
                    throw new Exception("Engine not found in database");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var engine = await _context.Engines.FindAsync(id);
            _context.Engines.Remove(engine);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Engine> FindAsync(int id)
        {
            var entity = await _context.Engines.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<Engine>> FindAllAsync()
        {
            var entities = await _context.Engines.ToListAsync();
            return entities;
        }
    }
}
