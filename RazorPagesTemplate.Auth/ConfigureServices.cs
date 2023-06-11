using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPagesTemplate.Auth.Infrastructure;

namespace RazorPagesTemplate.Auth;

public static class ConfigureServices
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var assembly = Assembly.GetExecutingAssembly();
        //_ = services.AddControllersWithViews()
        //    .AddApplicationPart(assembly)
        //    .AddRazorRuntimeCompilation();

        _ = services
            .AddRazorPages();

        _ = configuration.GetConnectionString("AuthConnection")
            ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");

        _ = services.AddHealthChecks()
            .AddDbContextCheck<AuthDbContext>();

        var serviceProvider = services.BuildServiceProvider();
        var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            _ = services.AddDatabaseDeveloperPageExceptionFilter();
        }

        _ = services.AddAuthentication();

        _ = services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AuthConnection"),
                builder => builder.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        _ = services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>();

        _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //services.AddScoped<IAuthDbContext>(provider => provider.GetRequiredService<AuthDbContext>());

        return services;
    }
}
