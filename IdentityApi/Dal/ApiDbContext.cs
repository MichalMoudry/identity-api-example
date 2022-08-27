namespace IdentityApi.Dal;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data source=identity.db");
}