using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformWriteDto, Platform>();
        CreateMap<PlatformReadDto, PlatformPublishedDto>();

        CreateMap<Platform, GrpcPlatformModel>()
            .ForMember(x => 
                x.PlatformId, opt => 
                    opt.MapFrom(y => y.Id))
            .ForMember(x =>
                x.Name, opt =>
                    opt.MapFrom(y => y.Name))
            .ForMember(x =>
                x.Publisher, opt =>
                    opt.MapFrom(y => y.Publisher));
    }
}