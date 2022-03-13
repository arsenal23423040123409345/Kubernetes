using PlatformService.Dtos;

namespace PlatformService.DataServices.Async.MessageBus;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
}
