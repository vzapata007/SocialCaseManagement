using alacnz.server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alacnz.server.Data.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<Session> GetByIdAsync(int id);
        Task AddAsync(Session session);
        Task UpdateAsync(Session session);
        Task DeleteAsync(int id);
    }
}
