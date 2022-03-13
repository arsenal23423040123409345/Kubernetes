using MediatR;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.DataServices.Async.MessageBus;
using PlatformService.DataServices.Sync.gRPC;
using PlatformService.DataServices.Sync.Http;

namespace PlatformService;

public class Startup
{
    public IConfigurationRoot Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public Startup(IConfigurationRoot configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        if (Environment.IsProduction())
        {
            Console.WriteLine("--> Using SQL Server DB");

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("PlatformsConn")));
        }
        else
        {
            Console.WriteLine("--> Using InMem DB");

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("InMem"));
        }

        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddSingleton<IMessageBusClient, MessageBusClient>();

        services.AddGrpc();

        services.AddHttpClient<ICommandDataClient, CommandDataClient>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<GrpcPlatformService>();

            endpoints.MapGet("/protos/platforms.proto", async context =>
            {
                await context.Response.WriteAsync(await File.ReadAllTextAsync("Protos/platforms.proto"));
            });
        });
    }
}
