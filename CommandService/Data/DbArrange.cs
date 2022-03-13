using CommandService.DataServices.Sync.gRPC;
using CommandService.Models;

namespace CommandService.Data;

public static class DbArrange
{
    public static async Task PopulateData(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = grpcClient.ReturnAllPlatforms();

        await SeedData(serviceScope.ServiceProvider.GetService<ICommandRepository>(), platforms);
    }

    private static async Task SeedData(ICommandRepository? repository, IEnumerable<Platform>? platforms)
    {
        Console.WriteLine("--> Seeding new platforms");

        if (platforms is not null && platforms.Any())
        {
            foreach (var platform in platforms)
            {
                if (!await repository.ExternalPlatformExistsAsync(platform.ExternalId))
                {
                    repository.CreatePlatformAsync(platform);
                }
            }
        }
    }
}
