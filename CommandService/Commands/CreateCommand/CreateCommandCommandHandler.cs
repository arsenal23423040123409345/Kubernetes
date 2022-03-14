using CommandService.Data;
using MediatR;

namespace CommandService.Commands.CreateCommand;

public class CreateCommandCommandHandler : IRequestHandler<CreateCommandCommand, Unit>
{
    private readonly ICommandRepository _repository;

    public CreateCommandCommandHandler(ICommandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateCommandCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.PlatformExistAsync(request.PlatformId))
        {
            throw new ArgumentNullException(nameof(request.Command.PlatformId));
        }

        request.Command.PlatformId = request.PlatformId;

        await _repository.CreateCommandAsync(request.PlatformId, request.Command);

        return Unit.Value;
    }
}
