using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Locations;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            this.locationRepository = locationRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddLocation(AddLocationDto locationDto)
        {
            var location = mapper.Map<Location>(locationDto);
            await locationRepository.AddAsync(location);
            return new SuccessResult("Location added");
        }

        public async Task<Result> DeleteLocation(LocationDto locationDto)
        {
            var location = mapper.Map<Location>(locationDto);
            await locationRepository.RemoveAsync(location);
            return new SuccessResult("Location deleted");
        }

        public DataResult<IQueryable<Location>> GetAllLocationsAsQueryable()
        {
            var locationList = locationRepository.GetAll();
            return new SuccessDataResult<IQueryable<Location>>(locationList);
        }

        public DataResult<List<LocationDto>> GetLocationListByCityId(int cityId)
        {
            var locationList = locationRepository.GetWhere(s => s.CityId == cityId).Include(i => i.City.Country).ToList();
            var locationListDto = mapper.Map<List<LocationDto>>(locationList);
            locationListDto.ForEach(locationDto =>
            {
                var location = locationList.First(s => s.Id == locationDto.Id);
                locationDto.CityName = location.City.Name;
                locationDto.CountryName = location.City.Country.Name;
                locationDto.CountryId = location.City.Country.Id;
            });
            return new SuccessDataResult<List<LocationDto>>("Locations of the city listed", locationListDto);
        }

        public async Task<DataResult<LocationDto>> UpdateLocation(UpdateLocationDto locationDto, int locationId)
        {
            var locationEntity = await locationRepository.GetByIdAsync(locationId);
            if (locationEntity != null)
            {
                locationEntity.Name = locationDto.Name ?? locationEntity.Name;
                locationEntity.Detail = locationDto.Detail ?? locationEntity.Detail;

                await locationRepository.Update(locationEntity);
                var mappedLocation = mapper.Map<LocationDto>(locationEntity);
                return new SuccessDataResult<LocationDto>("Location updated", mappedLocation);
            }
            return new ErrorDataResult<LocationDto>("Location couldn't update", null);
        }

        public DataResult<List<LocationDto>> GetAllLocationList()
        {
            var locationList = locationRepository.GetAll().Include(i => i.City.Country).ToList();
            var locationListDto = mapper.Map<List<LocationDto>>(locationList);
            foreach (var location in locationListDto)
            {
                var tmpLocationEntity = locationList.First(s => s.Id == location.Id);
                location.CityName = tmpLocationEntity.City.Name;
                location.CountryName = tmpLocationEntity.City.Country.Name;
                location.CountryId = tmpLocationEntity.City.Country.Id;
            }
            return new SuccessDataResult<List<LocationDto>>("All locations listed", locationListDto);
        }

        public async Task<DataResult<LocationDto>> GetLocationById(int locationId)
        {
            var location = await locationRepository.GetWhere(s => s.Id == locationId).Include(i => i.City.Country).FirstOrDefaultAsync();
            var locationDto = mapper.Map<LocationDto>(location);
            if (locationDto != null)
            {
                locationDto.CityName = location.City.Name;
                locationDto.CountryName = location.City.Country.Name;
                locationDto.CountryId = location.City.Country.Id;
                return new SuccessDataResult<LocationDto>("Location information listed", locationDto);
            }
            return new ErrorDataResult<LocationDto>("Location couldn't listed", null);
        }
    }
}
