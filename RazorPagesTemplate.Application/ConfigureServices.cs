using Microsoft.Extensions.DependencyInjection;

namespace RazorPagesTemplate.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
