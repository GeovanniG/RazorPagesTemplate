using Microsoft.Extensions.FileProviders;
using RazorPagesTemplate.Application;
using RazorPagesTemplate.Auth;
using RazorPagesTemplate.Infrastructure;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        _ = builder.Services.AddRazorPages();

        _ = builder.Services.AddApplicationServices();
        _ = builder.Services.AddAuthServices(builder.Configuration);
        _ = builder.Services.AddInfrastructureServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            _ = app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            _ = app.UseHsts();
        }

        _ = app.UseHealthChecks("/health");
        _ = app.UseHttpsRedirection();
        _ = app.UseStaticFiles();
        _ = app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "RazorPagesTemplate.Auth", "wwwroot"))
        });

        _ = app.UseRouting();

        _ = app.UseAuthorization();

        _ = app.MapRazorPages();

        await app.RunAsync();
    }
}