using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.Results;
using IdentityApi.Infrastructure;
using IdentityApi.Models;
using IdentityApi.Validators;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Users") ?? "Data Source=identity.db";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
_ = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSqlite<ApiDbContext>(connectionString)
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>();

// Indentity options config and JWT.
_ = builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
})
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidAudience = builder.Configuration["Audience"],
        ValidIssuer = builder.Configuration["Issuer"]
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger().UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    _ = app.UseHttpsRedirection().UseHsts();
}

// Register endpoint.
app.MapPost("/register", async ([FromBody] UserModel model, UserManager<IdentityUser> userManager) =>
{
    var validator = new UserModelValidator();
    var validationResult = validator.Validate(model);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors;
        return Results.BadRequest(CreateErrorMessage<ValidationFailure>(errors));
    }
    var user = new IdentityUser()
    {
        UserName = model.UserName,
        Email = model.Email
    };
    var createResult = await userManager.CreateAsync(user, model.Password);
    if (!createResult.Succeeded)
    {
        return Results.BadRequest();
    }
    var addToRoleResult = await userManager.AddToRoleAsync(user, "user");
    if (!addToRoleResult.Succeeded)
    {
        return Results.Problem();
    }
    return Results.Ok("User registered.");
}).WithName("Register").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest).ProducesProblem(500);

// Login endpoint.
app.MapGet("/login", async (
    [FromBody] UserModel model,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager) => {
    var validator = new UserModelValidator();
    var validationResult = validator.Validate(model);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors;
        return Results.BadRequest(CreateErrorMessage<ValidationFailure>(errors));
    }
    var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
    if (!signInResult.Succeeded)
    {
        return Results.BadRequest();
    }
    var user = await userManager.FindByEmailAsync(model.Email);
    var roles = await userManager.GetRolesAsync(user);
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.UserName)
    };
    foreach (var role in roles)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }
    return Results.Ok(); // TODO: return JWT token.
}).WithName("Login").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest).ProducesProblem(500);

app.Run();

/// <summary>
/// Method for creating an error message.
/// </summary>
/// <param name="errors">A collection of errors.</param>
string CreateErrorMessage<T>(IEnumerable<T>? errors)
{
    if (errors == null)
    {
        return "";
    }
    var stringBuilder = new StringBuilder().AppendJoin("\n", errors);
    return stringBuilder.ToString();
}