using CommandService.Data;
using CommandService.Models;
using MediatR;

namespace CommandService.Queries.GetCommand;

public class GetCommandQueryHandler : IRequestHandler<GetCommandQuery, Command?>
{
    private readonly ICommandRepository _repository;

    public GetCommandQueryHandler(ICommandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Command?> Handle(GetCommandQuery request, CancellationToken cancellationToken)
        => await _repository.PlatformExistAsync(request.PlatformId)
            ? await _repository.GetCommandAsync(request.PlatformId, request.CommandId)
            : null;
}
