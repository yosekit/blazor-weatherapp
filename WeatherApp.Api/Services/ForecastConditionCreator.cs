using AutoMapper;

using WeatherApp.Api.Models.Dtos;
using WeatherApp.Api.Models.Enumerations;

namespace WeatherApp.Api.Services
{
    public class ForecastConditionDtoCreator
    {
        private readonly IMapper _mapper;

        public ForecastConditionDtoCreator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ForecastConditionDto FromCode(int code)
        {
            // mapping by codes...

            var condition = ForecastCondition.Clearly;

            return _mapper.Map<ForecastConditionDto>(condition);
        }

        public IEnumerable<ForecastConditionDto> GetAll()
        {
            var dtos = new List<ForecastConditionDto>();

            var conditions = Enumeration.GetAll<ForecastCondition>();

            foreach (var condition in conditions)
                dtos.Add(_mapper.Map<ForecastConditionDto>(condition));

            return dtos;
        }
    }
}
