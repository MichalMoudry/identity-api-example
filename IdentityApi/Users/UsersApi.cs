using IdentityApi.Extensions;
using IdentityApi.Helpers;
using IdentityApi.Infrastructure.Repositories;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IdentityApi;

internal static class UsersApi
{
    public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group, string securityKey)
    {
        group.WithTags("Users");

        // Register route.
        group.MapPost("/register", async ([FromBody] RegisterModel model, IUserRepository userRepository) =>
        {
            var validationResult = model.Validate();
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors;
                return Results.BadRequest(RouteHelper.CreateErrorMessage(errors));
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
        })
        .WithName("Register").AddDefaultStatusCodes();

        // Login route.
        group.MapPost("/login", async (
            [FromBody] LoginModel model,
            IConfiguration configuration,
            IUserRepository userRepository,
            SignInManager<IdentityUser> signInManager) => 
        {
            var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (!signInResult.Succeeded)
            {
                return Results.BadRequest();
            }
            var (user, roles) = await userRepository.GetUserByUserName(model.UserName);
            var claims = RouteHelper.CreateClaimsForDefaultUser(user.Id, user.Email, user.UserName, roles);
            var signingKey = RouteHelper.CreateSigningKey(securityKey);
            return Results.Ok(new
            {
                User = user.Id,
                Token = RouteHelper.CreateJwtToken(configuration["Issuer"], configuration["Audience"], claims, signingKey)
            });
        })
        .WithName("Login").AddDefaultStatusCodes();

        // Edit account route.
        group.MapPut("/edit/{id}", [Authorize] (IUserRepository userRepository, string id) => {

        })
        .WithName("Edit account").RequireAuthorization().AddDefaultStatusCodes();

        // Delete account route.
        group.MapDelete("/delete/{id}", [Authorize] (IUserRepository userRepository, string id) => {
            
        })
        .WithName("Delete account").AddDefaultStatusCodes();

        // Reset password account route.
        group.MapPut("/resetpassword/{id}", [Authorize] async (string id, IUserRepository userRepository, [FromBody] PasswordResetModel model) => {
            var resetResult = await userRepository.ResetPassword(id, model.NewPassword);
            if (!resetResult.Succeeded)
            {
                return Results.BadRequest();
            }
            return Results.Ok("Password was reset.");
        })
        .WithName("Reset password").AddDefaultStatusCodes();

        return group;
    }
}