namespace IdentityApi.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : IdentityDbContext
{
    public ApiDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        _ = builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "user", NormalizedName = "USER" });
        _ = builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "admin", NormalizedName = "ADMIN" });
        base.OnModelCreating(builder);
    }
}