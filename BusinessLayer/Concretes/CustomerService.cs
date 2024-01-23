using AutoMapper;
using BusinessLayer.Abstracts;
using BusinessLayer.Dtos.Customers;
using Core.Utilities.Cloud;
using Core.Utilities.Results;
using DataAccessLayer.Abstracts;
using EntityLayer.Concretes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Concretes
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICloudRepo cloudRepo;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ICloudRepo cloudRepo)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.cloudRepo = cloudRepo;
        }

        public async Task<Result> AddCustomer(AddCustomerDto customer)
        {
            var customerEntity = mapper.Map<Customer>(customer);
            if (customerEntity != null)
            {
                await customerRepository.AddAsync(customerEntity);
                return new SuccessResult("Customer added");
            }
            return new ErrorResult("Customer couldn't added");
        }

        public async Task<Result> DeleteCustomer(CustomerDto customer)
        {
            var entity = mapper.Map<Customer>(customer);
            await customerRepository.SetActivity(entity, false);
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
            var customerEntity = await customerRepository.GetWhere(s => s.Id == customerId).Include(i => i.User).FirstOrDefaultAsync();
            if (customerEntity != null)
            {
                customerEntity.User.Email = customer.Email != null ? customer.Email : customerEntity.User.Email;
                customerEntity.User.PhoneNumber = customer.CellPhone != null ? customer.CellPhone : customerEntity.User.PhoneNumber;
                if (customer.ProfilePhoto != null)
                {
                    var existPhoto = await cloudRepo.GetFileWithUrl(customerEntity.User.ProfilePhotoUrl);
                    if (existPhoto != null)
                    {
                        List<string> publicIdList = [existPhoto.PublicId];
                        await cloudRepo.DeleteFileAsync(publicIdList);
                    }
                    var assetId = await cloudRepo.UploadFileAsync(customer.ProfilePhoto, Core.Enums.FileTypesEnum.Image);
                    var photoUrl = await cloudRepo.GetFileUrlAsync(assetId);
                    customerEntity.User.ProfilePhotoUrl = photoUrl;
                }
                await customerRepository.Update(customerEntity);
                var customerDto = mapper.Map<CustomerDto>(customerEntity);
                return new SuccessDataResult<CustomerDto>("Customer infortmation updated", customerDto);
            }
            return new ErrorDataResult<CustomerDto>("Customer couldn't found", null);
        }

        public DataResult<CustomerDto> GetCustomerByUserId(int userId)
        {
            var customer = customerRepository.GetWhere(s => s.UserId == userId).Include(i => i.User).FirstOrDefault();
            if (customer != null)
            {
                var customerDto = mapper.Map<CustomerDto>(customer);
                customerDto.Email = customer.User.Email;
                customerDto.CellPhone = customer.User.PhoneNumber;
                customerDto.ProfilePhoto = customer.User.ProfilePhotoUrl;
                return new SuccessDataResult<CustomerDto>(customerDto);
            }
            return new ErrorDataResult<CustomerDto>(null);
        }
    }
}
