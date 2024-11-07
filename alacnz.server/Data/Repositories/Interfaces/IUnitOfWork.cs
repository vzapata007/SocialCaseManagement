namespace alacnz.server.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICasoRepository Casos { get; }
        IClienteRepository Clientes { get; }
        IAlertaRepository Alertas { get; }
        Task<int> CompleteAsync();
    }
}
