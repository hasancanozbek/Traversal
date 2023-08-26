
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class CityRepository : GenericRepository<City>
    {
        public CityRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
