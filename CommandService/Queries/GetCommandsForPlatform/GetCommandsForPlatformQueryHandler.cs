using CommandService.Data;
using CommandService.Models;
using MediatR;

namespace CommandService.Queries.GetCommandsForPlatform;

public class GetCommandsForPlatformQueryHandler : IRequestHandler<GetCommandsForPlatformQuery, List<Command>?>
{
    private readonly ICommandRepository _repository;

    public GetCommandsForPlatformQueryHandler(ICommandRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Command>?> Handle(GetCommandsForPlatformQuery request, CancellationToken cancellationToken)
        => await _repository.PlatformExistAsync(request.PlatformId)
            ? await _repository.GetCommandsForPlatformAsync(request.PlatformId)
            : null;
}
