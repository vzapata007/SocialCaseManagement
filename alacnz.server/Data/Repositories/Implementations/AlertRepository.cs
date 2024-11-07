using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class AlertRepository : Repository<Alert>, IAlertRepository
    {
        public AlertRepository(AppDbContext context) : base(context) { }
    }
}
