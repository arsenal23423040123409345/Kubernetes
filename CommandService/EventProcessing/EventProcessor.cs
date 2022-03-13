using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }

    public async Task ProcessEventAsync(string message)
    {
        Console.WriteLine("--> Determining Event");

        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.PlatformPublished:
                await AddPlatform(message);
                break;
            case EventType.Undetermined:
                break;
        }
    }

    private static EventType DetermineEvent(string message)
        => JsonSerializer.Deserialize<GenericEventDto>(message)?.Event switch
        {
            "Platform_Published" => EventType.PlatformPublished,
            _ => EventType.Undetermined,
        };

    private async Task AddPlatform(string platformPublishedMessage)
    {
        using var scoped = _scopeFactory.CreateScope();

        var repository = scoped.ServiceProvider.GetService<ICommandRepository>();
        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

        try
        {
            var platform = _mapper.Map<Platform>(platformPublishedDto);

            if (!await repository.ExternalPlatformExistsAsync(platform.ExternalId))
            {
                repository.CreatePlatformAsync(platform);

                Console.WriteLine("--> Platform added successfully");
            }
            else
            {
                Console.WriteLine("--> Platform already exists");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not add platform to DB: {e.Message}");
        }
    }

    internal enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
