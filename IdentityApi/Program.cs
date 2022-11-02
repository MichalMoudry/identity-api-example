using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using IdentityApi.Infrastructure;
using IdentityApi.Models;
using IdentityApi.Validators;
using IdentityApi.Helpers;
using IdentityApi.Extensions;
using IdentityApi.Infrastructure.Repositories.Api;
using IdentityApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var endpointHelper = new RouteHelper();
var signingKey = endpointHelper.CreateSigningKey(builder.Configuration["SecurityKey"]);

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration["ConnectionString"]
    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (connectionString == null)
{
    throw new NullReferenceException("Connection string to DB is null.");
}

// Add services to the container.
builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString))
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>();

// Identity options config and JWT.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidAudience = builder.Configuration["Audience"],
        ValidIssuer = builder.Configuration["Issuer"],
        IssuerSigningKey = signingKey,
    };
});

var app = builder.Build();
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

// Register route.
app.MapPost("/register", async ([FromBody] UserModel model, IUserRepository userRepository) =>
{
    var validator = new UserModelValidator();
    var validationResult = validator.Validate(model);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors;
        return Results.BadRequest(endpointHelper.CreateErrorMessage(errors));
    }
    var (createResult, user) = await userRepository.CreateUserAsync(model.UserName, model.Email, model.Password);
    if (!createResult.Succeeded)
    {
        return Results.BadRequest();
    }
    var addToRoleResult = await userRepository.AddUserToRolesAsync(user, "user");
    if (!addToRoleResult.Succeeded)
    {
        return Results.Problem();
    }
    return Results.Ok("User registered.");
}).WithName("Register").AddDefaultStatusCodes();

// Login route.
app.MapPost("/login", async ([FromBody] UserModel model, IUserRepository userRepository, SignInManager<IdentityUser> signInManager) => {
    var validator = new UserModelValidator();
    var validationResult = validator.Validate(model);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors;
        return Results.BadRequest(endpointHelper.CreateErrorMessage(errors));
    }
    var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
    if (!signInResult.Succeeded)
    {
        return Results.BadRequest();
    }
    var (user, roles) = await userRepository.GetUserByEmailAsync(model.Email);
    var claims = endpointHelper.CreateClaimsForDefaultUser(user.Id, user.Email, user.UserName, roles);
    return Results.Ok(
        endpointHelper.CreateJwtToken(builder.Configuration["Issuer"], builder.Configuration["Audience"], claims, signingKey)
    );
}).WithName("Login").AddDefaultStatusCodes();

// Edit account route.
app.MapPost("/edit", () => {

}).WithName("Edit account").AddDefaultStatusCodes();

// Reset password account route.
app.MapPost("/resetpassword", () => {

}).WithName("Reset password").AddDefaultStatusCodes();

app.Run();