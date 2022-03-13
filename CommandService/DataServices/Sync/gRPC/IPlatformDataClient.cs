using CommandService.Models;

namespace CommandService.DataServices.Sync.gRPC;

public interface IPlatformDataClient
{
    IEnumerable<Platform>? ReturnAllPlatforms();
}
