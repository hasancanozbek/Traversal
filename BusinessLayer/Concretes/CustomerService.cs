using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Customers;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;

namespace BusinessLayer.Concretes
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<Result> AddCustomer(AddCustomerDto customer)
        {
            var customerEntity = mapper.Map<Customer>(customer);
            customerEntity.PasswordHash = customer.Password;
            await customerRepository.AddAsync(customerEntity);
            return new SuccessResult("Customer added");
        }

        public async Task<Result> DeleteCustomer(CustomerDto customer)
        {
            var customerEntity = mapper.Map<Customer>(customer);
            await customerRepository.RemoveAsync(customerEntity);
            return new SuccessResult("Customer deleted");
        }

        public DataResult<IQueryable<Customer>> GetAllCustomersAsQueryable()
        {
            var customers = customerRepository.GetAll();
            return new SuccessDataResult<IQueryable<Customer>>(customers);
        }

        public DataResult<List<CustomerDto>> GetAllCustomerList()
        {
            var customerList = customerRepository.GetAll().ToList();
            var mappedList = mapper.Map<List<CustomerDto>>(customerList);
            return new SuccessDataResult<List<CustomerDto>>("Returned customer list", mappedList);
        }

        public async Task<DataResult<CustomerDto>> GetCustomerById(int customerId)
        {
            var customer = await customerRepository.GetByIdAsync(customerId);
            var customerDto = mapper.Map<CustomerDto>(customer);
            return new SuccessDataResult<CustomerDto>("Customer information brought", customerDto);
        }

        public async Task<DataResult<CustomerDto>> UpdateCustomer(int customerId, UpdateCustomerDto customer)
        {
            var customerEntity = await customerRepository.GetByIdAsync(customerId);
            if (customerEntity != null)
            {
                customerEntity.Email = customer.Email ?? customerEntity.Email;
                customerEntity.PasswordHash = customer.Password ?? customerEntity.PasswordHash;
                //customerEntity.CellPhone = customer.CellPhone ?? customerEntity.CellPhone;
                var customerDto = mapper.Map<CustomerDto>(customerEntity);
                customerRepository.Update(customerEntity);
                return new SuccessDataResult<CustomerDto>("Customer infortmation updated", customerDto);
            }
            return new ErrorDataResult<CustomerDto>("Customer couldn't found", null);
        }
    }
}
