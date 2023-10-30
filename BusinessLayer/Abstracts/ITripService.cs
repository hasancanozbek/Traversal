using BusinessLayer.Dtos.Trips;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ITripService
    {
        DataResult<List<TripDto>> GetAllTripList();
        DataResult<IQueryable<Trip>> GetAllTripsAsQueryable();
        Task<DataResult<TripDto>> GetTripById(int tripId);
        Task<DataResult<TripDto>> UpdateTrip(UpdateTripDto trip, int tripId);
        Task<Result> AddTrip(AddTripDto trip);
        Task<Result> DeleteTrip(TripDto trip);
    }
}
