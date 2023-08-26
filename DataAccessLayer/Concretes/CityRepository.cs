
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
