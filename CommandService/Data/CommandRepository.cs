using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data;

public class CommandRepository : ICommandRepository
{
    private readonly AppDbContext _context;

    public CommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateCommandAsync(int platformId, Command command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        command.PlatformId = platformId;

        await _context.Commands.AddAsync(command);
        await _context.SaveChangesAsync();
    }

    public async Task CreatePlatformAsync(Platform platform)
    {
        if (platform is null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        await _context.Platforms.AddAsync(platform);
        await _context.SaveChangesAsync();
    }

    public Task<List<Platform>> GetAllPlatformsAsync() 
        => _context.Platforms.ToListAsync();

    public Task<Command?> GetCommandAsync(int platformId, int commandId) 
        => _context.Commands
            .FirstOrDefaultAsync(x => x.PlatformId == platformId && x.Id == commandId);

    public Task<List<Command>> GetCommandsForPlatformAsync(int platformId) 
        => _context.Commands
            .Where(x => x.PlatformId == platformId)
            .OrderBy(x => x.Platform.Name)
            .ToListAsync();

    public Task<bool> PlatformExistAsync(int platformId) 
        => _context.Platforms.AnyAsync(x => x.Id == platformId);

    public Task<bool> ExternalPlatformExistsAsync(int externalPlatformId) 
        => _context.Platforms.AnyAsync(x => x.ExternalId == externalPlatformId);
}
