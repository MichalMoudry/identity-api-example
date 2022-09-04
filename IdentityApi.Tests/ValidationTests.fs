module ValidationTests

open Xunit
open IdentityApi.Models
open IdentityApi.Validators

[<Fact>]
let ``Login model validation`` () =
    let validator = new LoginModelValidator()
    let testData = dict [
        new UserModel(UserName = "test1@test.com", Email = "test1@test.com", Password = "Q3KTRQG!GLL"), true
        new UserModel(UserName = "@test.com", Email = "test2@test.com", Password = "Q3KTRQG!GLL"), false
        new UserModel(UserName = "", Email = "test3@test.com", Password = "Q3KTRQG!GLL"), false
        new UserModel(UserName = "test4@test.com", Email = "test4@test.com", Password = ""), false
        new UserModel(UserName = "t@t.com", Email = "test5@test.com", Password = "1"), false
    ]
    for dataItem in testData do
        Assert.Equal(dataItem.Value, validator.Validate(dataItem.Key).IsValid)