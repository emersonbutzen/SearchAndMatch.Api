using Microsoft.EntityFrameworkCore;
using SearchAndMatch.DAL.Context;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Domain.Interfaces;

namespace SearchAndMatch.DAL.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        protected SearchAndMatchContext _context;
        public PatientRepository(SearchAndMatchContext context) {
            _context = context;
        }

        private bool PatientExists(int id)
        {
            return (_context.Patients?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<bool> AddAsync(Patient entity)
        {
            _context.Patients.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Patient entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(entity.Id))
                {
                    throw new Exception("Patient not found in database");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Patient> FindAsync(int id)
        {
            var entity = await _context.Patients.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<Patient>> FindAllAsync()
        {
            var entities = await _context.Patients.ToListAsync();
            return entities;
        }
    }
}
