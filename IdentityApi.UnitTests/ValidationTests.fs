module ValidationTests

open AssertFunctions
open System.Collections.Generic
open IdentityApi.Validators
open IdentityApi.Models
open Xunit

/// Method for testing user model validation.
[<Fact>]
[<Trait("Category", "UnitTest")>]
let UserModelValidation () =
    let validator = new UserModelValidator()
    let testData = new Dictionary<UserModel, bool>();
    testData.Add(new UserModel(UserName = "test1", Email = "test1@test.com", Password = "Q3KTRQG!GLL"), true)
    testData.Add(new UserModel(UserName = "test2", Email = "@test.com", Password = "Q3KTRQG!GLL"), false)
    testData.Add(new UserModel(UserName = "", Email = "test3@test.com", Password = "Q3KTRQG!GLL"), false)
    testData.Add(new UserModel(UserName = "test4", Email = "test4@test.com", Password = ""), false)
    testData.Add(new UserModel(UserName = "t5", Email = "test5@test.com", Password = "1"), false)
    for item in testData do
        validator.Validate(item.Key).IsValid |> Equal item.Value
