using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.CustomerTrips;
using BusinessLayer.Dtos.Trips;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class CustomerTripService : ICustomerTripService
    {
        private readonly ICustomerTripRepository customerTripRepository;
        private readonly IMapper mapper;

        public CustomerTripService(ICustomerTripRepository customerTripRepository, IMapper mapper)
        {
            this.customerTripRepository = customerTripRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddCustomerTrip(AddCustomerTripDto customerTrip)
        {
            var tripEntity = mapper.Map<CustomerTrip>(customerTrip);
            await customerTripRepository.AddAsync(tripEntity);
            return new SuccessResult("Customer trip added");
        }

        public async Task<Result> DeleteCustomerTrip(CustomerTripDto customerTrip)
        {
            var customerTripEntity = mapper.Map<CustomerTrip>(customerTrip);
            await customerTripRepository.RemoveAsync(customerTripEntity);
            return new SuccessResult("Customer trip deleted");
        }

        public DataResult<List<CustomerTripDto>> GetAllCustomerTripList()
        {
            var customerTripList = customerTripRepository.GetAll().ToList();
            var customerTripListDto = mapper.Map<List<CustomerTripDto>>(customerTripList);
            foreach (var customerTrip in customerTripListDto)
            {
                customerTrip.CustomerFirstName = customerTripList.First(s => s.Id == customerTrip.Id).Customer.FirstName;
                customerTrip.CustomerLastName = customerTripList.First(s => s.Id == customerTrip.Id).Customer.LastName;
                customerTrip.CustomerEmail = customerTripList.First(s => s.Id == customerTrip.Id).Customer.Email;
                customerTrip.TripName = customerTripList.First(s => s.Id == customerTrip.Id).Trip.Title;
            }
            return new SuccessDataResult<List<CustomerTripDto>>("All customer trips listed", customerTripListDto);
        }

        public DataResult<IQueryable<CustomerTrip>> GetAllCustomerTripsAsQueryable()
        {
            var customerTripList = customerTripRepository.GetAll();
            return new SuccessDataResult<IQueryable<CustomerTrip>>(customerTripList);
        }

        public async Task<DataResult<CustomerTripDto>> GetCustomerTripById(int customerTripId)
        {
            var customerTrip = await customerTripRepository.GetByIdAsync(customerTripId);
            var customerTripDto = mapper.Map<CustomerTripDto>(customerTrip);
            customerTripDto.CustomerFirstName = customerTrip.Customer.FirstName;
            customerTripDto.CustomerLastName = customerTrip.Customer.LastName;
            customerTripDto.CustomerEmail = customerTrip.Customer.Email;
            customerTripDto.TripName = customerTrip.Trip.Title;
            return new SuccessDataResult<CustomerTripDto>("Trip information listed", customerTripDto);
        }

        public DataResult<List<CustomerTripDto>> GetCustomerTripListByCustomerId(int customerId)
        {
            var customerTrips = customerTripRepository.GetWhere(s => s.CustomerId == customerId).ToList();
            var customerTripsDto = mapper.Map<List<CustomerTripDto>>(customerTrips);
            customerTripsDto.ForEach(customerTrip =>
            {
                var tmpCustomerTrip = customerTrips.First(s => s.Id == customerTrip.Id);
                customerTrip.CustomerFirstName = tmpCustomerTrip.Customer.FirstName;
                customerTrip.CustomerLastName = tmpCustomerTrip.Customer.LastName;
                customerTrip.CustomerEmail = tmpCustomerTrip.Customer.Email;
                customerTrip.TripName = tmpCustomerTrip.Trip.Title;
            });
            return new SuccessDataResult<List<CustomerTripDto>>("All trip information of the customer is listed", customerTripsDto);
        }

        public DataResult<List<CustomerTripDto>> GetCustomerTripListByTripId(int tripId)
        {
            var customerTrips = customerTripRepository.GetWhere(s => s.TripId == tripId).ToList();
            var customerTripsDto = mapper.Map<List<CustomerTripDto>>(customerTrips);
            customerTripsDto.ForEach(customerTrip =>
            {
                var tmpCustomerTrip = customerTrips.First(s => s.Id == customerTrip.Id);
                customerTrip.CustomerFirstName = tmpCustomerTrip.Customer.FirstName;
                customerTrip.CustomerLastName = tmpCustomerTrip.Customer.LastName;
                customerTrip.CustomerEmail = tmpCustomerTrip.Customer.Email;
                customerTrip.TripName = tmpCustomerTrip.Trip.Title;
            });
            return new SuccessDataResult<List<CustomerTripDto>>("All trip information of the customer is listed", customerTripsDto);
        }

        public async Task<DataResult<CustomerTripDto>> UpdateCustomerTrip(AddCustomerTripDto customerTrip, int customerTripId)
        {
            var customerTripEntity = await customerTripRepository.GetByIdAsync(customerTripId);
            if (customerTripEntity != null)
            {
                customerTripEntity.CustomerId = customerTrip.CustomerId == 0 ? customerTripEntity.CustomerId : customerTrip.CustomerId;
                customerTripEntity.TripId = customerTrip.TripId == 0 ? customerTripEntity.TripId : customerTrip.TripId;
                await customerTripRepository.Update(customerTripEntity);
                var mappedCustomerTrip = mapper.Map<CustomerTripDto>(customerTripEntity);
                return new SuccessDataResult<CustomerTripDto>("Customer trip updated", mappedCustomerTrip);
            }
            return new ErrorDataResult<CustomerTripDto>("Customer trşp couldn't update", null);
        }
    }
}