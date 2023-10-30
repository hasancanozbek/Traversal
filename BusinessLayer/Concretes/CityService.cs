using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Cities;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;
        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddCity(AddCityDto city)
        {
            var cityEntity = mapper.Map<City>(city);
            await cityRepository.AddAsync(cityEntity);
            return new SuccessResult("City added");
        }

        public async Task<Result> DeleteCity(CityDto city)
        {
            var cityEntity = mapper.Map<City>(city);
            await cityRepository.RemoveAsync(cityEntity);
            return new SuccessResult("City deleted");
        }

        public DataResult<IQueryable<City>> GetAllCitiesAsQueryable()
        {
            var cityList = cityRepository.GetAll();
            return new SuccessDataResult<IQueryable<City>>(cityList);
        }

        public DataResult<List<CityDto>> GetAllCityList()
        {
            var cityList = cityRepository.GetAll();
            var cityListDto = mapper.Map<List<CityDto>>(cityList);
            return new SuccessDataResult<List<CityDto>>(cityListDto);
        }

        public async Task<DataResult<CityDto>> GetCityByCode(int code)
        {
            var city = await cityRepository.GetSingleAsync(s => s.Code == code);
            var cityDto = mapper.Map<CityDto>(city);
            return new SuccessDataResult<CityDto>("City information listed", cityDto);
        }

        public async Task<DataResult<CityDto>> UpdateCity(AddCityDto city, int cityId)
        {
            var cityEntity = await cityRepository.GetSingleAsync(s => s.Code == city.Code);
            if (cityEntity != null)
            {
                cityEntity.CountryId = city.CountryId == 0 ? cityEntity.CountryId : city.CountryId;
                cityEntity.Code = city.Code == 0 ? cityEntity.Code : city.Code;
                cityEntity.Name = city.Name ?? cityEntity.Name;
                await cityRepository.Update(cityEntity);
                var cityDto = mapper.Map<CityDto>(cityEntity);
                return new SuccessDataResult<CityDto>("City updated", cityDto);
            }
            return new ErrorDataResult<CityDto>("City couldn't updated", null);
        }
    }
}
