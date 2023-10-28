using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ILocationService
    {
        IQueryable<Location> GetAllLocationList();
        Task<Location> GetLocationById(int locationId);
    }
}
