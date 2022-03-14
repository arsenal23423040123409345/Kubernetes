using CommandService.Data;
using CommandService.Models;
using MediatR;

namespace CommandService.Queries.GetAllPlatforms;

public class GetAllPlatformsQueryHandler : IRequestHandler<GetAllPlatformsQuery, List<Platform>>
{
    private readonly ICommandRepository _repository;

    public GetAllPlatformsQueryHandler(ICommandRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Platform>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
        => _repository.GetAllPlatformsAsync();
}
