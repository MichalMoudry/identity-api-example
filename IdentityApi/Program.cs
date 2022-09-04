using System.Text;
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

app.MapPost("/register", async ([FromBody] RegisterModel model, UserManager<IdentityUser> userManager) =>
{
    var validator = new RegisterCommandValidator();
    var validationResult = validator.Validate(model);
    var sb = new StringBuilder();
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
        var errors = createResult.Errors.Select(e => e.Code);
        return Results.BadRequest(CreateErrorMessage<string>(errors));
    }
    var addToRoleResult = await userManager.AddToRoleAsync(user, "user");
    if (!addToRoleResult.Succeeded)
    {
        var errors = addToRoleResult.Errors.Select(e => e.Code);
        return Results.Problem(CreateErrorMessage<string>(errors));
    }
    return Results.Ok($"User count: {userManager.Users.Count()}");
})
.WithName("Register").Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest).ProducesProblem(500);

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