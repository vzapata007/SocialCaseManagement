using alacnz.server.Data.Repositories.Interfaces;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Casos = new CasoRepository(context);
            Clientes = new ClienteRepository(context);
            Alertas = new AlertaRepository(context);
        }

        public ICasoRepository Casos { get; private set; }
        public IClienteRepository Clientes { get; private set; }
        public IAlertaRepository Alertas { get; private set; }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
