using BusinessLayer.Abstracts;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IQueryable<Customer> GetAllCustomerList()
        {
            return customerRepository.GetAll();
        }

        public Task<Customer> GetCustomerById(int customerId)
        {
            return customerRepository.GetByIdAsync(customerId);
        }
    }
}
