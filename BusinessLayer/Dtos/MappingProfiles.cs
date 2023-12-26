using AutoMapper;
using BusinessLayer.Dtos.BlogComments;
using BusinessLayer.Dtos.Blogs;
using BusinessLayer.Dtos.Cities;
using BusinessLayer.Dtos.Comments;
using BusinessLayer.Dtos.Countries;
using BusinessLayer.Dtos.Customers;
using BusinessLayer.Dtos.CustomerTrips;
using BusinessLayer.Dtos.Guides;
using BusinessLayer.Dtos.Locations;
using BusinessLayer.Dtos.TripDates;
using BusinessLayer.Dtos.TripLocations;
using BusinessLayer.Dtos.Trips;
using EntityLayer.Concretes;

namespace BusinessLayer.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<TripComment, TripCommentDto>()
            .ForMember(dest => dest.CustomerFirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
            .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.Customer.LastName))
            .ForMember(dest => dest.TripName, opt => opt.MapFrom(src => src.TripDate.Trip.Title))
            .ForMember(dest => dest.TripDate, opt => opt.MapFrom(src => src.TripDate.Date))
            .ReverseMap();
            CreateMap<TripComment, AddTripCommentDto>().ReverseMap();
            CreateMap<TripComment, UpdateTripCommentDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, AddCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<Blog, AddBlogDto>().ReverseMap();
            CreateMap<Blog, UpdateBlogDto>().ReverseMap();

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, AddCityDto>().ReverseMap();

            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, AddCountryDto>().ReverseMap();

            CreateMap<CustomerTrip, CustomerTripDto>().ReverseMap();
            CreateMap<CustomerTrip, AddCustomerTripDto>().ReverseMap();

            CreateMap<Guide, GuideDto>().ReverseMap();
            CreateMap<Guide, AddGuideDto>().ReverseMap();
            CreateMap<Guide, UpdateGuideDto>().ReverseMap();
            
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Location, AddLocationDto>().ReverseMap();
            CreateMap<Location, UpdateLocationDto>().ReverseMap();

            CreateMap<TripLocation, AddTripLocationDto>().ReverseMap();

            CreateMap<Trip, TripDto>().ReverseMap();
            CreateMap<Trip, AddTripDto>().ReverseMap();
            CreateMap<Trip, UpdateTripDto>().ReverseMap();

            CreateMap<TripDate, TripDateDto>().ReverseMap();

            CreateMap<BlogComment, AddBlogCommentDto>().ReverseMap();
            CreateMap<BlogComment, UpdateBlogCommentDto>().ReverseMap();
            CreateMap<BlogComment, BlogCommentDto>().ReverseMap();
        }
    }
}
