using CommandService.Models;

namespace CommandService.Data;

public class CommandRepository : ICommandRepository
{
    private readonly AppDbContext _context;

    public CommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateCommand(int platformId, Command command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        command.PlatformId = platformId;

        await _context.Commands.AddAsync(command);
        await _context.SaveChangesAsync();
    }

    public async Task CreatePlatform(Platform platform)
    {
        if (platform is null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        await _context.Platforms.AddAsync(platform);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Platform> GetAllPlatforms() 
        => _context.Platforms.ToList();

    public Command? GetCommand(int platformId, int commandId) 
        => _context.Commands
            .FirstOrDefault(x => x.PlatformId == platformId && x.Id == commandId);

    public IEnumerable<Command> GetCommandsForPlatform(int platformId) 
        => _context.Commands
            .Where(x => x.PlatformId == platformId)
            .OrderBy(x => x.Platform.Name)
            .ToList();

    public bool PlatformExist(int platformId) 
        => _context.Platforms.Any(x => x.Id == platformId);

    public bool ExternalPlatformExists(int externalPlatformId) 
        => _context.Platforms.Any(x => x.ExternalId == externalPlatformId);
}
