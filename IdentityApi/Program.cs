using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityApi.Infrastructure;
using IdentityApi.Infrastructure.Repositories;
using IdentityApi;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var securityKey = builder.Environment.IsDevelopment()
    ? builder.Configuration["SecurityKey"]
    : Environment.GetEnvironmentVariable("SECURITY_KEY");
if (securityKey == null)
{
    throw new NullReferenceException("Security key is null.");
}

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration["ConnectionString"]
    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (connectionString == null)
{
    throw new NullReferenceException("Connection string to DB is null.");
}

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .Configure<SwaggerGeneratorOptions>(options => options.InferSecuritySchemes = true);
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString))
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApiDbContext>();

// Identity options config and JWT.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}
else
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.MapGroup("/users").MapUsers(securityKey);

app.Run();