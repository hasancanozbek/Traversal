using BusinessLayer.Abstracts;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class GuideService : IGuideService
    {
        private readonly IGuideRepository guideRepository;

        public GuideService(IGuideRepository guideRepository)
        {
            this.guideRepository = guideRepository;
        }

        public IQueryable<Guide> GetAllGuideList()
        {
            return guideRepository.GetAll();
        }

        public Task<Guide> GetGuideById(int guideId)
        {
            return guideRepository.GetByIdAsync(guideId);
        }
    }
}
