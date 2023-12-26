using Core.Utilities.Cloud;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICloudRepo, CloudinaryRepo>();
    }
}