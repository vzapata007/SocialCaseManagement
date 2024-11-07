using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.EntityFrameworkCore;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly AppDbContext _context;

        public BeneficiaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beneficiary>> GetAllAsync()
        {
            return await _context.Beneficiaries.ToListAsync();  // Ensure ToListAsync is used with the correct namespace
        }

        public async Task<Beneficiary> GetByIdAsync(int id)
        {
            return await _context.Beneficiaries.FindAsync(id);
        }

        public async Task AddAsync(Beneficiary beneficiary)
        {
            await _context.Beneficiaries.AddAsync(beneficiary);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Update(beneficiary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
                await _context.SaveChangesAsync();
            }
        }
    }
}
