using AutoMapper;
using CommandService.Commands.CreateCommand;
using CommandService.Dtos;
using CommandService.Models;
using CommandService.Queries.GetCommand;
using CommandService.Queries.GetCommandsForPlatform;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId:int}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CommandsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
    {
        var commands = await _mediator.Send(new GetCommandsForPlatformQuery(platformId));

        return commands is null
            ? NotFound()
            : Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{commandId:int}", Name = "GetCommandForPlatform")]
    public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
    {
        var command = await _mediator.Send(new GetCommandQuery(platformId, commandId));

        return command is not null
            ? Ok(_mapper.Map<CommandReadDto>(command))
            : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<CommandReadDto>> CreateCommandForPlatform(int platformId, [FromBody] CommandWriteDto commandWriteModel)
    {
        var command = _mapper.Map<Command>(commandWriteModel);

        try
        {
            await _mediator.Send(new CreateCommandCommand(command, platformId));
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }

        var commandReadDto = _mapper.Map<CommandReadDto>(command);

        return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
    }
}
