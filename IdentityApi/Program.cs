using Microsoft.AspNetCore.Identity;
using IdentityApi.Infrastructure;
using IdentityApi.Dispatchers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
_ = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<ApiDbContext>()
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>();

// Indentity options config.
_ = builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
});

var app = builder.Build();
var commandDispatcher = new CommandDispatcher(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.Run();