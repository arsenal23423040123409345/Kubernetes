using MediatR;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Queries.GetPlatformById;

public class GetPlatformByIdQueryHandler : IRequestHandler<GetPlatformByIdQuery, Platform?>
{
    private readonly IPlatformRepository _repository;

    public GetPlatformByIdQueryHandler(IPlatformRepository repository)
    {
        _repository = repository;
    }

    public Task<Platform?> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
        => _repository.GetPlatformByIdAsync(request.PlatformId);

}
