using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
