using BusinessLayer.Dtos.Customers;
using Core.Utilities.Results;
using EntityLayer.Concretes;

namespace BusinessLayer.Abstracts
{
    public interface ICustomerService
    {
        DataResult<List<CustomerDto>> GetAllCustomerList();
        DataResult<IQueryable<Customer>> GetAllCustomersAsQueryable();
        Task<DataResult<CustomerDto>> GetCustomerById(int customerId);
        Task<Result> AddCustomer(AddCustomerDto customer);
        Task<DataResult<CustomerDto>> UpdateCustomer(int customerId, UpdateCustomerDto customer);
        DataResult<CustomerDto> GetCustomerByUserId(int userId);
        Task<Result> DeleteCustomer(CustomerDto customer);
    }
}
