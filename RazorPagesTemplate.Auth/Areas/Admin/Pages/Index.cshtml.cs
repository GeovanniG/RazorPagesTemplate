using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorPagesTemplate.Auth.Areas.Admin.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IndexModel(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnGetAsync()
    {
        var user = await GetSupportedUserAsync();
        if (user is null) { return; }

        var adminRole = "Admin";
        await CreateRoleAsync(adminRole);

        _ = await _userManager.AddToRoleAsync(user, adminRole);
    }

    private async Task<IdentityUser?> GetSupportedUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) { return null; }

        var user = await _userManager.FindByIdAsync(userId);
        var supportedEmails = new List<string>() { "test@gmail.com" };

        if (user is null
            || !supportedEmails.Any(email => string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase)))
        {
            return null;
        }

        return user;
    }

    private async Task CreateRoleAsync(string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            _ = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = role
            });
        }
    }
}
