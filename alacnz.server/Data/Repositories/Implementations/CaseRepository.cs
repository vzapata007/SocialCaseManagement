using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(AppDbContext context) : base(context) { }
    }
}
