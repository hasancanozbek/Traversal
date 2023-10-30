using BusinessLayer.Dtos.Locations;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ILocationService
    {
        DataResult<IQueryable<Location>> GetAllLocationsAsQueryable();
        DataResult<List<LocationDto>> GetAllLocationList();
        Task<DataResult<LocationDto>> GetLocationById(int locationId);
        Task<DataResult<List<LocationDto>>> GetLocationListByCityId(int cityId);
        Task<DataResult<LocationDto>> UpdateLocation(UpdateLocationDto locationDto, int locationId);
        Task<Result> AddLocation(AddLocationDto locationDto);
        Task<Result> DeleteLocation(LocationDto locationDto);
    }
}
