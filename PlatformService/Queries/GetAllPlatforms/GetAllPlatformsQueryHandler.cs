using MediatR;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Queries.GetAllPlatforms;

public class GetAllPlatformsQueryHandler : IRequestHandler<GetAllPlatformsQuery, List<Platform>>
{
    private readonly IPlatformRepository _repository;

    public GetAllPlatformsQueryHandler(IPlatformRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Platform>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
        => _repository.GetAllPlatformsAsync();
}
