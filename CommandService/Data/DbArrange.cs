using CommandService.Models;
using CommandService.SyncDataServices.gRPC;

namespace CommandService.Data;

public static class DbArrange
{
    public static void PopulateData(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = grpcClient.ReturnAllPlatforms();

        SeedData(serviceScope.ServiceProvider.GetService<ICommandRepository>(), platforms);
    }

    private static void SeedData(ICommandRepository? repository, IEnumerable<Platform>? platforms)
    {
        Console.WriteLine("--> Seeding new platforms");

        if (platforms is not null && platforms.Any())
        {
            foreach (var platform in platforms)
            {
                if (!repository.ExternalPlatformExists(platform.ExternalId))
                {
                    repository.CreatePlatform(platform);
                }
            }
        }
    }
}
