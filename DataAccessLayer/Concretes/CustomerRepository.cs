
using DataAccessLayer.Abstracts;
using DataAccessLayer.EntityFrameworkCore;
using EntityLayer.Concretes;

namespace DataAccessLayer.Concretes
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(TraversalDbContext context) : base(context)
        {
        }
    }
}
