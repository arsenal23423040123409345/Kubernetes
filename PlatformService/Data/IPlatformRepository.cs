using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepository
{
    Task<List<Platform>> GetAllPlatformsAsync();

    Task<Platform?> GetPlatformByIdAsync(int id);

    Task CreatePlatformAsync(Platform platform);
}
