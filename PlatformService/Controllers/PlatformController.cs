using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Commands.CreatePlatform;
using PlatformService.DataServices.Async.MessageBus;
using PlatformService.DataServices.Sync.Http;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Queries.GetAllPlatforms;
using PlatformService.Queries.GetPlatformById;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMessageBusClient _messageBusClient;
    private readonly IMediator _mediator;

    public PlatformController(
        IMapper mapper,
        ICommandDataClient commandDataClient,
        IMessageBusClient messageBusClient,
        IMediator mediator)
    {
        _mapper = mapper;
        _commandDataClient = commandDataClient;
        _messageBusClient = messageBusClient;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAllPlatforms()
    {
        var platforms = await _mediator.Send(new GetAllPlatformsQuery());

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id:int}", Name = "GetPlatformByIdAsync")]
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
    {
        var platform = await _mediator.Send(new GetPlatformByIdQuery(id));

        return platform is not null
            ? Ok(_mapper.Map<PlatformReadDto>(platform))
            : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformWriteDto platformWriteDto)
    {
        var platformModel = _mapper.Map<Platform>(platformWriteDto);

        await _mediator.Send(new CreatePlatformCommand(platformModel));

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        // Send Sync message
        try
        {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not send synchronously {e.Message}");
        }

        // Send Async message
        try
        {
            var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
            platformPublishedDto.Event = "Platform_Published";

            _messageBusClient.PublishNewPlatform(platformPublishedDto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not send synchronously {e.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { platformReadDto.Id }, platformReadDto);
    }
}