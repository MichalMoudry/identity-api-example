module ValidationTests

open Xunit
open IdentityApi.Models
open IdentityApi.Validators

[<Fact>]
let ``Register model validation`` () =
    let validator = new RegisterModelValidator()
    let testData = dict [
        new RegisterModel(UserName = "Test username 1", Email = "test1@test.com", Password = "95UQTVQ!LTWtP"), true
        new RegisterModel(UserName = "T2", Email = "test2@test.com", Password = "95UQTVQ!LTWtP"), false
        new RegisterModel(UserName = "", Email = "test3@test.com", Password = "95UQTVQ!LTWtP"), false
        new RegisterModel(UserName = "Test username 4", Email = "test4@test.com", Password = "95UQTVQ!LTWtP"), true
    ]
    for dataItem in testData do
        Assert.Equal(dataItem.Value, validator.Validate(dataItem.Key).IsValid)

[<Fact>]
let ``Login model validation`` () =
    let validator = new LoginModelValidator()
    let testData = dict [
        new LoginModel(Email = "test1@test.com", Password = "Q3KTRQG!GLL"), true
        new LoginModel(Email = "@test.com", Password = "Q3KTRQG!GLL"), false
        new LoginModel(Email = "", Password = "Q3KTRQG!GLL"), false
        new LoginModel(Email = "test4@test.com", Password = ""), false
    ]
    for dataItem in testData do
        Assert.Equal(dataItem.Value, validator.Validate(dataItem.Key).IsValid)