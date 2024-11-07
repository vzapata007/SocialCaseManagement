using alacnz.server.Data.Repositories.Implementations;
using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Cases = new CaseRepository(context);
        Clients = new ClientRepository(context);
        Alerts = new AlertRepository(context);
        SocialWorkTeams = new SocialWorkTeamRepository(context);
        Services = new ServiceRepository(context);
        Sessions = new SessionRepository(context);
        Beneficiaries = new BeneficiaryRepository(context); // Beneficiary repository added
    }

    public ICaseRepository Cases { get; private set; }
    public IClientRepository Clients { get; private set; }
    public IAlertRepository Alerts { get; private set; }
    public ISocialWorkTeamRepository SocialWorkTeams { get; private set; }
    public IServiceRepository Services { get; private set; }
    public ISessionRepository Sessions { get; private set; }
    public IBeneficiaryRepository Beneficiaries { get; private set; } // Property for Beneficiaries

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}