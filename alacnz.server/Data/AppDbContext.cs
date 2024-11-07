using Microsoft.EntityFrameworkCore;
using alacnz.server.Models;
using System.Linq;

namespace alacnz.server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SocialWorkTeam> SocialWorkTeams { get; set; }
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Case-Client Relationship (One-to-Many)
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Client)
                .WithMany(client => client.Cases)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Case-SocialWorkTeam Relationship (One-to-Many)
            modelBuilder.Entity<Case>()
                .HasOne(c => c.SocialWorkTeam)
                .WithMany(swt => swt.Cases)
                .HasForeignKey(c => c.SocialWorkTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Service-Case Relationship (One-to-Many)
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Cases)
                .WithOne(c => c.Service)
                .HasForeignKey(c => c.ServiceId);

            // Case-Session Relationship (One-to-Many)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Case)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
