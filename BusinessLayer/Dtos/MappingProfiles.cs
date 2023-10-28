using AutoMapper;
using BusinessLayer.Dtos.Comments;
using BusinessLayer.Dtos.Customers;
using EntityLayer.Concretes;

namespace BusinessLayer.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Customer, AddCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
