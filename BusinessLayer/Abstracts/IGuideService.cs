using BusinessLayer.Dtos.Guides;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface IGuideService
    {
        DataResult<IQueryable<Guide>> GetAllGuidesAsQueryable();
        DataResult<List<GuideDto>> GetAllGuideList();
        Task<DataResult<GuideDto>> GetGuideById(int guideId);
        Task<Result>AddGuide(AddGuideDto guide);
        Task<DataResult<GuideDto>> UpdateGuide(UpdateGuideDto guide, int id);
        Task<Result> DeleteGuide(GuideDto guide);
    }
}
