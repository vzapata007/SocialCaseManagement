using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class CasoRepository : Repository<Caso>, ICasoRepository
    {
        public CasoRepository(AppDbContext context) : base(context) { }
    }
}
