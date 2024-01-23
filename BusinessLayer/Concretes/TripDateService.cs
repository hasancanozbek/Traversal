using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.TripDates;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class TripDateService : ITripDateService
    {
        private readonly ITripDateRepository tripDateRepository;
        private readonly IMapper mapper;

        public TripDateService(ITripDateRepository tripDateRepository, IMapper mapper)
        {
            this.tripDateRepository = tripDateRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddTripDate(AddTripDateDto tripDate)
        {
            var tripDateEntity = mapper.Map<TripDate>(tripDate);
            await tripDateRepository.AddAsync(tripDateEntity);
            return new SuccessResult("Trip date added");
        }

        public async Task<Result> DeleteTripDate(TripDateDto tripDate)
        {
            var tripDateEntity = mapper.Map<TripDate>(tripDate);
            await tripDateRepository.RemoveAsync(tripDateEntity);
            return new SuccessResult("Trip date deleted");
        }

        public DataResult<List<TripDateDto>> GetAllTripDateList()
        {
            var tripDateList = tripDateRepository.GetAll().Include(i => i.Trip).ToList();
            var tripDateListDto = mapper.Map<List<TripDateDto>>(tripDateList);
            tripDateListDto.ForEach(tripDateDto =>
            {
                var tripDate = tripDateList.First(s => s.Id == tripDateDto.Id);
                tripDateDto.TripDay = tripDate.Trip.Day;
                tripDateDto.TripPrice = tripDate.Trip.Price;
                tripDateDto.TripTitle = tripDate.Trip.Title;
            });
            return new SuccessDataResult<List<TripDateDto>>("All trip dates listed", tripDateListDto);
        }

        public DataResult<IQueryable<TripDate>> GetAllTripDatesAsQueryable()
        {
            var tripDateList = tripDateRepository.GetAll();
            return new SuccessDataResult<IQueryable<TripDate>>(tripDateList);
        }

        public DataResult<List<TripDateDto>> GetAllTripDaysOfTripById(int tripId)
        {
            var tripDateList = tripDateRepository.GetWhere(s => s.TripId == tripId).Include(i => i.Trip).ToList();
            var tripDateDtoList = mapper.Map<List<TripDateDto>>(tripDateList);
            tripDateDtoList.ForEach(tripDateDto =>
            {
                var tripDate = tripDateList.First();
                tripDateDto.TripDay = tripDate.Trip.Day;
                tripDateDto.TripPrice = tripDate.Trip.Price;
                tripDateDto.TripTitle = tripDate.Trip.Title;
            });
            return new SuccessDataResult<List<TripDateDto>>("Trip date information listed", tripDateDtoList);
        }

        public async Task<DataResult<TripDateDto>> GetTripDateById(int tripDateId)
        {
            var tripDate = await tripDateRepository.GetWhere(s => s.Id == tripDateId).Include(i => i.Trip).FirstOrDefaultAsync();
            var tripDateDto = mapper.Map<TripDateDto>(tripDate);
            if (tripDate != null)
            {
                tripDateDto.TripDay = tripDate.Trip.Day;
                tripDateDto.TripPrice = tripDate.Trip.Price;
                tripDateDto.TripTitle = tripDate.Trip.Title;
            }
            return new SuccessDataResult<TripDateDto>("Trip date information listed", tripDateDto);
        }

        public async Task SetActive(TripDate tripDate, bool isActive)
        {
            if (tripDate != null)
            {
                await tripDateRepository.SetActivity(tripDate, isActive);
            }
        }

        public async Task<DataResult<TripDateDto>> UpdateTripDate(UpdateTripDateDto tripDate, int tripDateId)
        {
            var tripDateEntity = await tripDateRepository.GetByIdAsync(tripDateId);
            if (tripDateEntity != null)
            {
                tripDateEntity.Date = tripDate.Date ?? tripDateEntity.Date;
                tripDateEntity.Quota = tripDate.Quota == 0 ? tripDateEntity.Quota : tripDate.Quota;
                await tripDateRepository.Update(tripDateEntity);
                var mappedTripDate = mapper.Map<TripDateDto>(tripDateEntity);
                return new SuccessDataResult<TripDateDto>("Trip date updated", mappedTripDate);
            }
            return new ErrorDataResult<TripDateDto>("Trip date couldn't update", null);
        }
    }
}
