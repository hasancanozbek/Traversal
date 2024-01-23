using BusinessLayer.Dtos.CustomerTrips;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICustomerTripService
    {
        DataResult<List<CustomerTripDto>> GetAllCustomerTripList();
        DataResult<IQueryable<CustomerTrip>> GetAllCustomerTripsAsQueryable();
        Task<DataResult<CustomerTripDto>> GetCustomerTripById(int customerTripId);
        DataResult<List<CustomerTripDto>> GetCustomerTripListByCustomerId(int customerId);
        DataResult<List<CustomerTripDto>> GetCustomerTripListByTripId(int tripId);
        Task<DataResult<CustomerTripDto>> UpdateCustomerTrip(AddCustomerTripDto customerTrip, int customerTripId);
        Task<Result> AddCustomerTrip(AddCustomerTripDto customerTrip);
        Task<Result> DeleteCustomerTrip(CustomerTripDto customerTrip);
        bool IsParticipantToTrip(int customerId, int tripId);
        bool IsRightToComment(int customerId, int tripId);
        Task<Result> Reservation(int tripDateId, int customerId);
    }
}
