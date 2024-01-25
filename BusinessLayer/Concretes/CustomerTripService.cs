using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.CustomerTrips;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class CustomerTripService : ICustomerTripService
    {
        private readonly ICustomerTripRepository customerTripRepository;
        private readonly ITripCommentService tripCommentService;
        private readonly ITripDateService tripDateService;
        private readonly IMapper mapper;

        public CustomerTripService(ICustomerTripRepository customerTripRepository, IMapper mapper, ITripCommentService tripCommentService, ITripDateService tripDateService)
        {
            this.customerTripRepository = customerTripRepository;
            this.mapper = mapper;
            this.tripCommentService = tripCommentService;
            this.tripDateService = tripDateService;
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
            var customerTripList = customerTripRepository.GetAll().Include(i => i.Customer).Include(i => i.TripDate.Trip).ToList();
            var customerTripListDto = mapper.Map<List<CustomerTripDto>>(customerTripList);
            foreach (var customerTrip in customerTripListDto)
            {
                customerTrip.CustomerFirstName = customerTripList.First(s => s.Id == customerTrip.Id).Customer.FirstName;
                customerTrip.CustomerLastName = customerTripList.First(s => s.Id == customerTrip.Id).Customer.LastName;
                customerTrip.TripName = customerTripList.First(s => s.Id == customerTrip.Id).TripDate.Trip.Title;
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
            var customerTrip = await customerTripRepository.GetWhere(s => s.Id == customerTripId).Include(i => i.Customer).Include(i => i.TripDate.Trip).FirstOrDefaultAsync();
            var customerTripDto = mapper.Map<CustomerTripDto>(customerTrip);
            if (customerTripDto != null)
            {
                customerTripDto.CustomerFirstName = customerTrip.Customer.FirstName;
                customerTripDto.CustomerLastName = customerTrip.Customer.LastName;
                customerTripDto.TripName = customerTrip.TripDate.Trip.Title;
                return new SuccessDataResult<CustomerTripDto>("Customer trip information listed", customerTripDto);
            }
            return new ErrorDataResult<CustomerTripDto>("Customer trip information couldn't found", null);
        }

        public DataResult<List<CustomerTripDto>> GetCustomerTripListByCustomerId(int customerId)
        {
            var customerTrips = customerTripRepository.GetWhere(s => s.CustomerId == customerId).Include(i => i.Customer).Include(i => i.TripDate.Trip).ToList();
            var customerTripsDto = mapper.Map<List<CustomerTripDto>>(customerTrips);
            customerTripsDto.ForEach(customerTrip =>
            {
                var tmpCustomerTrip = customerTrips.First(s => s.Id == customerTrip.Id);
                customerTrip.CustomerFirstName = tmpCustomerTrip.Customer.FirstName;
                customerTrip.CustomerLastName = tmpCustomerTrip.Customer.LastName;
                customerTrip.TripName = tmpCustomerTrip.TripDate.Trip.Title;
                customerTrip.Date = tmpCustomerTrip.TripDate.Date;
                customerTrip.Day = tmpCustomerTrip.TripDate.Trip.Day;
                customerTrip.TripId = tmpCustomerTrip.TripDate.TripId;
            });
            return new SuccessDataResult<List<CustomerTripDto>>("All trip information of the customer is listed", customerTripsDto);
        }

        public DataResult<List<CustomerTripDto>> GetCustomerTripListByTripId(int tripId)
        {
            var customerTripDateIdList = tripDateService.GetAllTripDaysOfTripById(tripId).Data.Select(s => s.Id).ToList();
            var customerTrips = customerTripRepository.GetWhere(s => customerTripDateIdList.Contains(s.TripDateId)).Include(i => i.Customer).Include(i => i.TripDate.Trip).ToList();
            var customerTripsDto = mapper.Map<List<CustomerTripDto>>(customerTrips);
            customerTripsDto.ForEach(customerTrip =>
            {
                var tmpCustomerTrip = customerTrips.First(s => s.Id == customerTrip.Id);
                customerTrip.CustomerFirstName = tmpCustomerTrip.Customer.FirstName;
                customerTrip.CustomerLastName = tmpCustomerTrip.Customer.LastName;
                customerTrip.TripName = tmpCustomerTrip.TripDate.Trip.Title;
            });
            return new SuccessDataResult<List<CustomerTripDto>>("All trip information of the customer is listed", customerTripsDto);
        }

        public bool IsParticipantToTrip(int customerId, int tripId)
        {
            var result = GetCustomerTripListByCustomerId(customerId);
            if (result.IsSuccess)
            {
                var isParticipant = result.Data.Any(s => s.TripDateId == tripId);
                return isParticipant;
            }
            return false;
        }

        public bool IsRightToComment(int customerId, int tripId)
        {
            var participantResult = IsParticipantToTrip(customerId, tripId);
            var commentResult = tripCommentService.GetCommentListOfCustomerById(customerId);
            if (participantResult && commentResult.IsSuccess)
            {
                var tripDateIdList = tripDateService.GetAllTripDaysOfTripById(tripId).Data.Select(s => s.Id);
                var isRightToComment = commentResult.Data.Any(s => tripDateIdList.Contains(s.TripDateId));
                return !isRightToComment;
            }
            return false;
        }

        public async Task<Result> Reservation(int tripDateId, int customerId)
        {
            var tripDate = await tripDateService.GetTripDateById(tripDateId);
            if (tripDate != null)
            {
                var customerTripData = new AddCustomerTripDto()
                {
                    CustomerId = customerId,
                    TripDateId = tripDateId,
                    Price = tripDate.Data.TripPrice
                };
                return await AddCustomerTrip(customerTripData);
            }
            return new ErrorResult("Reservation couldn't saved");
        }

        public async Task<DataResult<CustomerTripDto>> UpdateCustomerTrip(AddCustomerTripDto customerTrip, int customerTripId)
        {
            var customerTripEntity = await customerTripRepository.GetByIdAsync(customerTripId);
            if (customerTripEntity != null)
            {
                customerTripEntity.CustomerId = customerTrip.CustomerId == 0 ? customerTripEntity.CustomerId : customerTrip.CustomerId;
                customerTripEntity.TripDateId = customerTrip.TripDateId == 0 ? customerTripEntity.TripDateId : customerTrip.TripDateId;
                await customerTripRepository.Update(customerTripEntity);
                var mappedCustomerTrip = mapper.Map<CustomerTripDto>(customerTripEntity);
                return new SuccessDataResult<CustomerTripDto>("Customer trip updated", mappedCustomerTrip);
            }
            return new ErrorDataResult<CustomerTripDto>("Customer trip couldn't update", null);
        }
    }
}