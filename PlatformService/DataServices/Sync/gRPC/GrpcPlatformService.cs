using AutoMapper;
using Grpc.Core;
using MediatR;
using PlatformService.Data;
using PlatformService.Queries.GetAllPlatforms;

namespace PlatformService.DataServices.Sync.gRPC;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GrpcPlatformService(IPlatformRepository repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        Console.WriteLine("--> Request was Received");

        var response = new PlatformResponse();

        var platforms = await _mediator.Send(new GetAllPlatformsQuery());

        foreach (var platform in platforms)
        {
            response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
        }

        return response;
    }
}
