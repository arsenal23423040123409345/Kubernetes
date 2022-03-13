using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId:int}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepository _repository;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
    {
        Console.WriteLine($"--> Hit GetCommandsForPlatformAsync: {platformId}");

        if (!await _repository.PlatformExistAsync(platformId))
        {
            return NotFound();
        }

        var commands = _repository.GetCommandsForPlatformAsync(platformId);

        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{commandId:int}", Name = "GetCommandForPlatform")]
    public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
    {
        Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

        if (!await _repository.PlatformExistAsync(platformId))
        {
            return NotFound();
        }

        var command = await _repository.GetCommandAsync(platformId, commandId);

        return command is not null
            ? Ok(_mapper.Map<CommandReadDto>(command))
            : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<CommandReadDto>> CreateCommandForPlatform(int platformId, [FromBody] CommandWriteDto commandWriteModel)
    {
        Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

        if (!await _repository.PlatformExistAsync(platformId))
        {
            return NotFound();
        }

        var command = _mapper.Map<Command>(commandWriteModel);

        await _repository.CreateCommandAsync(platformId, command);

        var commandReadDto = _mapper.Map<CommandReadDto>(command);

        return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
    }
}
