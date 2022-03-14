using CommandService.Models;
using MediatR;

namespace CommandService.Queries.GetCommand;

public record GetCommandQuery(int PlatformId, int CommandId) : IRequest<Command?>;
