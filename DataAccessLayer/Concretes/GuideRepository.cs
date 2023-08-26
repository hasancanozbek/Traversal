
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class GuideRepository : GenericRepository<Guide>, IGuideRepository
    {
        public GuideRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
