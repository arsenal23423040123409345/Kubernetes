using CommandService.Models;

namespace CommandService.Data;

public interface ICommandRepository
{
    // Platforms
    Task<List<Platform>> GetAllPlatformsAsync();

    Task CreatePlatformAsync(Platform platform);

    Task<bool> PlatformExistAsync(int platformId);

    Task<bool> ExternalPlatformExistsAsync(int externalPlatformId);

    // Commands
    Task<List<Command>> GetCommandsForPlatformAsync(int platformId);

    Task<Command?> GetCommandAsync(int platformId, int commandId);

    Task CreateCommandAsync(int platformId, Command command);
}
