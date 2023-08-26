
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class CustomerTripRepository : GenericRepository<CustomerTrip>, ICustomerTripRepository
    {
        public CustomerTripRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
