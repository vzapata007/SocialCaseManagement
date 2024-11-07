using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Interfaces
{
    public interface IBeneficiaryRepository
    {
        Task<IEnumerable<Beneficiary>> GetAllAsync();
        Task<Beneficiary> GetByIdAsync(int id);
        Task AddAsync(Beneficiary beneficiary);
        Task UpdateAsync(Beneficiary beneficiary);
        Task DeleteAsync(int id);
    }
}
