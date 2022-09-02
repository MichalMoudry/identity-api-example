using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityApi.Infrastructure;
using IdentityApi.Commands;
using IdentityApi.Validators;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger().UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    _ = app.UseHttpsRedirection().UseHsts();
}

app.MapPost("/register", ([FromBody] RegisterCommand command, UserManager<IdentityUser> userManager) =>
{
    var validator = new RegisterCommandValidator();
    var validationResult = validator.Validate(command);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors;
        var message = new StringBuilder().AppendJoin('\n', errors);
        
        return message.ToString();
    }
    /*
    var user = new IdentityUser()
    {
        UserName = command.UserName,
        Email = command.Email
    };
    await _userManager.CreateAsync(user, command.Password);
    var result = await _userManager.AddToRoleAsync(user, "user");
    */
    Console.WriteLine($"{command.Email} | {command.UserName} | {command.Password}");
    return $"User count: {userManager.Users.Count()}";
});

app.Run();