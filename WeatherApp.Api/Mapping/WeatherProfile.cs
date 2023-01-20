using AutoMapper;
using WeatherApp.Api.Models.Dtos;
using WeatherApp.Api.Models.Enumerations;

namespace WeatherApp.Api.Mapping
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<ForecastCondition, ForecastConditionDto>()
            .ForMember(dest => dest.Code, act => act.MapFrom(src => src.Value))
            .ForMember(dest => dest.Text, act => act.MapFrom(src => src.Description))
            .ForMember(dest => dest.Icon, act => act.MapFrom(src => src.IconName));
        }
    }
}
