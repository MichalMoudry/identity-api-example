namespace IdentityApi.Infrastructure;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : IdentityDbContext
{
    /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data source=identity.db");*/
    public ApiDbContext(DbContextOptions options) : base(options) {}
}