using CommandService.Models;
using MediatR;

namespace CommandService.Queries.GetCommandsForPlatform;

public record GetCommandsForPlatformQuery(int PlatformId) : IRequest<List<Command>?>;

