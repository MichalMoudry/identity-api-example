namespace IdentityApi.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public interface IApiDbContext
{
    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }

    public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }

    public DbSet<IdentityUserLogin<string>> UserLogins { get; set; }

    public DbSet<IdentityUserRole<string>> UserRoles { get; set; }

    public DbSet<IdentityUser> Users { get; set; }
}