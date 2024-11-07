using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class AlertaRepository : Repository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(AppDbContext context) : base(context) { }
    }
}
