using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface IGuideService
    {
        IQueryable<Guide> GetAllGuideList();
        Task<Guide> GetGuideById(int guideId);
    }
}
