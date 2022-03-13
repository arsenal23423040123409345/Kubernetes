using AutoMapper;
using CommandService.Data;
using CommandService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.SyncDataServices.gRPC;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public PlatformDataClient(IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public IEnumerable<Platform>? ReturnAllPlatforms()
    {
        Console.WriteLine($"--> Calling gRPC Service {_configuration["GrpcPlatform"]}");

        var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try
        {
            var response = client.GetAllPlatforms(request);

            return _mapper.Map<IEnumerable<Platform>>(response.Platform);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Couldn't call gRPC server: {e.Message}");

            return null;
        }
    }
}
