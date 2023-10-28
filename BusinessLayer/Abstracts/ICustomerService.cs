using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetAllCustomerList();
        Task<Customer> GetCustomerById(int customerId);
    }
}
