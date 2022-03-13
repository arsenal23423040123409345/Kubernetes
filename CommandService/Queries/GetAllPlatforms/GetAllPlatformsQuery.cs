using CommandService.Models;
using MediatR;

namespace CommandService.Queries.GetAllPlatforms;

public record GetAllPlatformsQuery : IRequest<List<Platform>>;
