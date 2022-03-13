using MediatR;
using PlatformService.Models;

namespace PlatformService.Commands.CreatePlatform;

public record CreatePlatformCommand(Platform Platform) : IRequest<Unit>;