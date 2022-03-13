using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _dbContext;

    public PlatformRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreatePlatformAsync(Platform platform)
    {
        await _dbContext.Platforms.AddAsync(platform);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Platform>> GetAllPlatformsAsync()
        => _dbContext.Platforms.ToListAsync();

    public Task<Platform?> GetPlatformByIdAsync(int id)
        => _dbContext.Platforms.FirstOrDefaultAsync(x => x.Id == id);
}
