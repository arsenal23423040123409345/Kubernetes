using CommandService.Models;

namespace CommandService.Data;

public interface ICommandRepository
{
    // Platforms
    IEnumerable<Platform> GetAllPlatforms();

    Task CreatePlatform(Platform platform);

    bool PlatformExist(int platformId);

    bool ExternalPlatformExists(int externalPlatformId);

    // Commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);

    Command? GetCommand(int platformId, int commandId);

    Task CreateCommand(int platformId, Command command);
}
