using BusinessLayer.Dtos.TripDates;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ITripDateService
    {
        DataResult<List<TripDateDto>> GetAllTripDateList();
        DataResult<IQueryable<TripDate>> GetAllTripDatesAsQueryable();
        Task<DataResult<TripDateDto>> GetTripDateById(int tripDateId);
        DataResult<List<TripDateDto>> GetAllTripDaysOfTripById(int tripId);
        Task<DataResult<TripDateDto>> UpdateTripDate(UpdateTripDateDto tripDate, int tripDateId);
        Task<Result> AddTripDate(AddTripDateDto tripDate);
        Task<Result> DeleteTripDate(TripDateDto tripDate);
    }
}
