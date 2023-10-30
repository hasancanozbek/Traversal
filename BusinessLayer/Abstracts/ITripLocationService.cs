using BusinessLayer.Dtos.Locations;
using BusinessLayer.Dtos.TripLocations;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ITripLocationService
    {
        DataResult<IQueryable<TripLocation>> GetAllTripLocationsAsQueryable();
        DataResult<List<TripLocation>> GetAllTripLocationList();
        Task<DataResult<TripLocation>> GetTripLocationById(int tripLocationId);
        DataResult<List<TripLocation>> GetTripLocationListByTripId(int tripId);
        DataResult<List<TripLocation>> GetTripLocationListByLocationId(int locationId);
        Task<DataResult<LocationDto>> UpdateTripLocation(AddTripLocationDto tripLocationDto, int tripLocationId);
        Task<Result> AddTripLocation(AddTripLocationDto tripLocationDto);
        Task<Result> DeleteTripLocation(TripLocation tripLocation);
    }
}
