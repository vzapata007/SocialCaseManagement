using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context) { }
    }
}
