namespace IdentityApi.Tests;

using IdentityApi.Validators;
using IdentityApi.Models;

/// <summary>
/// Class containing methods for testing validations.
/// </summary>
public sealed class ValidationTests
{
    /// <summary>
    /// Test method for testing user model validation.
    /// </summary>
    [Fact]
    public void UserModelValidation()
    {
        var validator = new UserModelValidator();
        var testData = new Dictionary<UserModel, bool>
        {
            { new UserModel() { UserName = "test1", Email = "test1@test.com", Password = "Q3KTRQG!GLL" }, true },
            { new UserModel() { UserName = "test2", Email = "@test.com", Password = "Q3KTRQG!GLL" }, false },
            { new UserModel() { UserName = "", Email = "test3@test.com", Password = "Q3KTRQG!GLL" }, false },
            { new UserModel() { UserName = "test4", Email = "test4@test.com", Password = "" }, false },
            { new UserModel() { UserName = "t5", Email = "test5@test.com", Password = "1" }, false }
        };

        foreach (var dataItem in testData)
        {
            _ = validator.Validate(dataItem.Key).IsValid.Should().Be(dataItem.Value);
        }
    }
}