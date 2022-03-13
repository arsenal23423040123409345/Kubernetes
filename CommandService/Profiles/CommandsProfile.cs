using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using PlatformService;

namespace CommandService.Profiles;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandWriteDto, Command>();
        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(x => 
                x.ExternalId, opt => 
                    opt.MapFrom(y => y.Id));
        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(x =>
                x.ExternalId, opt => 
                    opt.MapFrom(y => y.PlatformId))
            .ForMember(x =>
                x.Name, opt =>
                    opt.MapFrom(y => y.Name))
            .ForMember(x =>
                x.Commands, opt =>
                    opt.Ignore()); ;
    }
}
