﻿using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Trips;
using Core.Enums;
using Core.Utilities.Cloud;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class TripService : ITripService
    {
        private readonly ITripRepository tripRepository;
        private readonly IMapper mapper;
        private readonly ICloudRepo cloudRepo;
        private readonly ITripKeyRepository tripKeyRepository;

        public TripService(ITripRepository tripRepository, IMapper mapper, ICloudRepo cloudRepo, ITripKeyRepository tripKeyRepository)
        {
            this.tripRepository = tripRepository;
            this.mapper = mapper;
            this.cloudRepo = cloudRepo;
            this.tripKeyRepository = tripKeyRepository;
        }

        public async Task<Result> AddTrip(AddTripDto trip)
        {
            var tripEntity = mapper.Map<Trip>(trip);
            var tripId = await tripRepository.AddAsync(tripEntity);
            foreach (var image in trip.ImageList)
            {
                var fileAssetId = await cloudRepo.UploadFileAsync(@image, FileTypesEnum.Image);
                if (!fileAssetId.Equals(string.Empty))
                {
                    var keyValuePair = new TripKey()
                    {
                        TripId = tripId,
                        Key = BlogKeysEnum.image.ToString(),
                        Value = fileAssetId
                    };
                    await tripKeyRepository.AddAsync(keyValuePair);
                }
            }
            return new SuccessResult("Trip added");
        }

        public async Task<Result> DeleteTrip(TripDto trip)
        {
            var tripEntity = mapper.Map<Trip>(trip);
            await tripRepository.RemoveAsync(tripEntity);
            return new SuccessResult("Trip deleted");
        }

        public DataResult<IQueryable<Trip>> GetAllTripsAsQueryable()
        {
            var tripList = tripRepository.GetAll();
            return new SuccessDataResult<IQueryable<Trip>>(tripList);
        }

        public DataResult<List<TripDto>> GetAllTripList()
        {
            var tripList = tripRepository.GetAll().Include(i => i.Guide).ToList();
            var tripListDto = mapper.Map<List<TripDto>>(tripList);
            foreach (var trip in tripListDto)
            {
                trip.GuideFirstName = tripList.First(s => s.Id == trip.Id).Guide.FirstName;
                trip.GuideLastName = tripList.First(s => s.Id == trip.Id).Guide.LastName;
                trip.ImageList = new List<string>();
                var imageKeys = tripKeyRepository.GetWhere(s => s.TripId == trip.Id && s.Key == BlogKeysEnum.image.ToString()).OrderBy(o => o.CreatedTime).Select(s => s.Value).ToList();
                foreach (var imageId in imageKeys)
                {
                    var url = cloudRepo.GetFileUrl(imageId);
                    trip.ImageList.Add(url);
                }
            }
            return new SuccessDataResult<List<TripDto>>("All trips listed", tripListDto);
        }

        public async Task<DataResult<TripDto>> GetTripById(int tripId)
        {
            var trip = await tripRepository.GetWhere(s => s.Id == tripId).Include(i => i.Guide).FirstOrDefaultAsync();
            var tripDto = mapper.Map<TripDto>(trip);
            if (trip != null)
            {
                tripDto.GuideFirstName = trip.Guide.FirstName;
                tripDto.GuideLastName = trip.Guide.LastName;
                tripDto.ImageList = new List<string>();
                var imageKeys = tripKeyRepository.GetWhere(s => s.TripId == trip.Id && s.Key == BlogKeysEnum.image.ToString()).OrderBy(o => o.CreatedTime).Select(s => s.Value).ToList();
                foreach (var imageId in imageKeys)
                {
                    var url = cloudRepo.GetFileUrl(imageId);
                    tripDto.ImageList.Add(url);
                }
            }
            return new SuccessDataResult<TripDto>("Trip information listed", tripDto);
        }

        public async Task<DataResult<TripDto>> UpdateTrip(UpdateTripDto trip, int tripId)
        {
            var tripEntity = await tripRepository.GetByIdAsync(tripId);
            if (tripEntity != null)
            {
                tripEntity.Title = trip.Title ?? tripEntity.Title;
                tripEntity.Content = trip.Content ?? tripEntity.Content;
                tripEntity.Price = trip.Price == 0 ? tripEntity.Price : trip.Price;
                tripEntity.Day = trip.Day == 0 ? tripEntity.Day : trip.Day;
                tripEntity.GuideId = trip.GuideId == 0 ? tripEntity.GuideId : trip.GuideId;
                await tripRepository.Update(tripEntity);
                var mappedTrip = mapper.Map<TripDto>(tripEntity);
                return new SuccessDataResult<TripDto>("Trip updated", mappedTrip);
            }
            return new ErrorDataResult<TripDto>("Trip couldn't update", null);
        }
    }
}
