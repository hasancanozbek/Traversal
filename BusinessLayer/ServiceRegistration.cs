using BusinessLayer.Abstracts;
using BusinessLayer.Concretes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceRegistration
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<ITripService, TripService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IGuideService, GuideService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ICustomerTripService, CustomerTripService>();
        services.AddScoped<ITripLocationService, TripLocationService>();
        services.AddScoped<ICommentService, CommentService>();
    }
}