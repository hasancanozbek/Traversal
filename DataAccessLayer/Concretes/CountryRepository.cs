
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class CountryRepository : GenericRepository<Country>
    {
        public CountryRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
