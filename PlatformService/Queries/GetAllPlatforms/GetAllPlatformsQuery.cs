using MediatR;
using PlatformService.Models;

namespace PlatformService.Queries.GetAllPlatforms;

public class GetAllPlatformsQuery : IRequest<List<Platform>>
{
}
