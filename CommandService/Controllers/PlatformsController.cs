using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Queries.GetAllPlatforms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly ICommandRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PlatformsController(ICommandRepository repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlatformReadDto>>> GetPlatforms()
    {
        var platforms = await _mediator.Send(new GetAllPlatformsQuery());

        return Ok(_mapper.Map<List<PlatformReadDto>>(platforms));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbound POST # Command Service");

        return Ok("Inbound test OK for Platforms Controller");
    }
}
