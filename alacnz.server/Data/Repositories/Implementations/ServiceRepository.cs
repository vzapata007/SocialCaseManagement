using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) { }
    }
}
