using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.EntityFrameworkCore;

namespace alacnz.server.Data.Repositories.Implementations
{
    public class SocialWorkTeamRepository : ISocialWorkTeamRepository
    {
        private readonly AppDbContext _context;

        public SocialWorkTeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SocialWorkTeam>> GetAllAsync()
        {
            return await _context.SocialWorkTeams.ToListAsync();  // Use proper method from DbContext
        }

        public async Task<SocialWorkTeam> GetByIdAsync(int id)
        {
            return await _context.SocialWorkTeams.FindAsync(id);  // Find the team by ID
        }

        public async Task AddAsync(SocialWorkTeam team)
        {
            await _context.SocialWorkTeams.AddAsync(team);  // Add the team to Db
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SocialWorkTeam team)
        {
            _context.SocialWorkTeams.Update(team);  // Update the team
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _context.SocialWorkTeams.FindAsync(id);  // Find the team by ID
            if (team != null)
            {
                _context.SocialWorkTeams.Remove(team);  // Remove it from the Db
                await _context.SaveChangesAsync();
            }
        }
    }
}
