using MediatR;
using PlatformService.Data;

namespace PlatformService.Commands.CreatePlatform;

public class CreatePlatformCommandHandler : IRequestHandler<CreatePlatformCommand, Unit>
{
    private readonly IPlatformRepository _repository;

    public CreatePlatformCommandHandler(IPlatformRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
    {
        if (request.Platform is null)
        {
            throw new ArgumentNullException(nameof(request.Platform));
        }

        await _repository.CreatePlatformAsync(request.Platform);

        return Unit.Value;
    }
}
