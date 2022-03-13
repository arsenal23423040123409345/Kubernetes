using MediatR;
using PlatformService.Models;

namespace PlatformService.Queries.GetAllPlatforms;

public record GetAllPlatformsQuery : IRequest<List<Platform>>;