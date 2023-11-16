using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Locations;
using BusinessLayer.Dtos.TripLocations;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class TripLocationService : ITripLocationService
    {
        private readonly ITripLocationRepository triplocationRepository;
        private readonly IMapper mapper;

        public TripLocationService(ITripLocationRepository triplocationRepository, IMapper mapper)
        {
            this.triplocationRepository = triplocationRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddTripLocation(AddTripLocationDto tripLocationDto)
        {
            var tripLocationEntity = mapper.Map<TripLocation>(tripLocationDto);
            await triplocationRepository.AddAsync(tripLocationEntity);
            return new SuccessResult("Trip location added");
        }

        public async Task<Result> DeleteTripLocation(TripLocation tripLocation)
        {
            await triplocationRepository.RemoveAsync(tripLocation);
            return new SuccessResult("Trip location deleted");
        }

        public DataResult<List<TripLocation>> GetAllTripLocationList()
        {
            var tripLocationList = triplocationRepository.GetAll().ToList();
            return new SuccessDataResult<List<TripLocation>>("All trip locations listed", tripLocationList);
        }

        public DataResult<IQueryable<TripLocation>> GetAllTripLocationsAsQueryable()
        {
            var tripLocationList = triplocationRepository.GetAll();
            return new SuccessDataResult<IQueryable<TripLocation>>(tripLocationList);
        }

        public async Task<DataResult<TripLocation>> GetTripLocationById(int tripLocationId)
        {
            var tripLocation = await triplocationRepository.GetByIdAsync(tripLocationId);
            return new SuccessDataResult<TripLocation>("Trip location information listed", tripLocation);
        }

        public DataResult<List<TripLocation>> GetTripLocationListByLocationId(int locationId)
        {
            var tripLocationList = triplocationRepository.GetWhere(s => s.LocationId == locationId).ToList();
            return new SuccessDataResult<List<TripLocation>>("All trip location information of location listed", tripLocationList);
        }

        public DataResult<List<TripLocation>> GetTripLocationListByTripId(int tripId)
        {
            var tripLocationList = triplocationRepository.GetWhere(s => s.TripId == tripId).ToList();
            return new SuccessDataResult<List<TripLocation>>("All trip location information of trip listed", tripLocationList);
        }

        public async Task<DataResult<TripLocation>> UpdateTripLocation(TripLocation tripLocation)
        {
            await triplocationRepository.Update(tripLocation);
            return new SuccessDataResult<TripLocation>("Trip updated", tripLocation);
        }
    }
}
