using CommandService.Models;
using MediatR;

namespace CommandService.Commands.CreateCommand;

public record CreateCommandCommand(Command Command, int PlatformId) : IRequest<Unit>;
