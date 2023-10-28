using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ITripService
    {
        IQueryable<Trip> GetAllTripList();
        Task<Trip> GetTripById(int tripId);
    }
}
