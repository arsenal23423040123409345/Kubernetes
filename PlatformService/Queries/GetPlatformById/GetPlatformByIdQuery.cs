using MediatR;
using PlatformService.Models;

namespace PlatformService.Queries.GetPlatformById;

public record GetPlatformByIdQuery(int PlatformId) : IRequest<Platform?>;

