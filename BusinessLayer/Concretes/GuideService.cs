using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Guides;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class GuideService : IGuideService
    {
        private readonly IGuideRepository guideRepository;
        private readonly IMapper mapper;

        public GuideService(IGuideRepository guideRepository, IMapper mapper)
        {
            this.guideRepository = guideRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddGuide(AddGuideDto guide)
        {
            var guideEntity = mapper.Map<Guide>(guide);
            await guideRepository.AddAsync(guideEntity);
            return new SuccessResult("Guide added");
        }

        public async Task<Result> DeleteGuide(GuideDto guide)
        {
            var entity = mapper.Map<Guide>(guide);
            await guideRepository.SetActivity(entity, false);
            return new SuccessResult("Guide deleted");
        }

        public DataResult<List<GuideDto>> GetAllGuideList()
        {
            var guideList = guideRepository.GetAll().ToList();
            var guideDtoList = mapper.Map<List<GuideDto>>(guideList);
            return new SuccessDataResult<List<GuideDto>>("All guides listed", guideDtoList);
        }

        public async Task<DataResult<GuideDto>> UpdateGuide(UpdateGuideDto guide, int id)
        {
            var guideEntity = await guideRepository.GetByIdAsync(id);
            if (guideEntity != null)
            {
                guideEntity.Description = guide.Description ?? guideEntity.Description;
                guideEntity.IsActive =
                await guideRepository.Update(guideEntity);
                var guideDto = mapper.Map<GuideDto>(guideEntity);
                return new SuccessDataResult<GuideDto>("Guide information updated", guideDto);
            }
            return new ErrorDataResult<GuideDto>("Guide couldn't found", null);
        }

        public DataResult<IQueryable<Guide>> GetAllGuidesAsQueryable()
        {
            var guideList = guideRepository.GetAll();
            return new SuccessDataResult<IQueryable<Guide>>("All guides listed", guideList);
        }

        public async Task<DataResult<GuideDto>> GetGuideById(int guideId)
        {
            var guide = await guideRepository.GetByIdAsync(guideId);
            if (guide != null)
            {
                var guideDto = mapper.Map<GuideDto>(guide);
                return new SuccessDataResult<GuideDto>("Guide listed", guideDto);
            }
            return new ErrorDataResult<GuideDto>("Guide couldn't found", null);
        }
    }
}
