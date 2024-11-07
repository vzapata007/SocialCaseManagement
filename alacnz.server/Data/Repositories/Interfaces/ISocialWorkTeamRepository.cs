using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Interfaces
{
    public interface ISocialWorkTeamRepository
    {
        Task<IEnumerable<SocialWorkTeam>> GetAllAsync();  
        Task<SocialWorkTeam> GetByIdAsync(int id);
        Task AddAsync(SocialWorkTeam team);
        Task UpdateAsync(SocialWorkTeam team);
        Task DeleteAsync(int id);
    }
}
