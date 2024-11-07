using alacnz.server.Data.Repositories.Implementations;

namespace alacnz.server.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICaseRepository Cases { get; }
        IClientRepository Clients { get; }
        IAlertRepository Alerts { get; }
        ISocialWorkTeamRepository SocialWorkTeams { get; }
        IServiceRepository Services { get; }
        IBeneficiaryRepository Beneficiaries { get; } 
        ISessionRepository Sessions { get; }

        Task<int> CompleteAsync();
    }
}
