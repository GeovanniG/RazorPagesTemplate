using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ResumeVault.Auth.Infrastructure;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        _ = builder.Entity<IdentityUser>().ToTable("users");
        _ = builder.Entity<IdentityRole>().ToTable("roles");
        _ = builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
        _ = builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
        _ = builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
        _ = builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
        _ = builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
    }
}
