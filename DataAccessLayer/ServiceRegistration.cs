using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes;
using DataAccessLayer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static void AddDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContext<TraversalDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IGuideRepository, GuideRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ICustomerTripRepository, CustomerTripRepository>();
        services.AddScoped<ITripLocationRepository, TripLocationRepository>();
        services.AddScoped<ITripCommentRepository, TripCommentRepository>();
        services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
        services.AddScoped<ITripDateRepository, TripDateRepository>();
        services.AddScoped<IBlogKeyRepository, BlogKeyRepository>();
        services.AddScoped<ITripKeyRepository, TripKeyRepository>();
    }
}

