module ValidationTests

open IdentityApi.Models
open IdentityApi.Validators
open Xunit

[<Fact>]
let ``Register command validation`` () =
    let validator = new RegisterModelValidator()
    let testData = [|
        new RegisterModel(UserName = "Test username 1", Email = "test1@test.com", Password = "95UQTVQ!LTWtP")
        new RegisterModel(UserName = "T2", Email = "test2@test.com", Password = "95UQTVQ!LTWtP")
        new RegisterModel(UserName = "", Email = "test3@test.com", Password = "95UQTVQ!LTWtP")
        new RegisterModel(UserName = "Test username 4", Email = "test4@test.com", Password = "95UQTVQ!LTWtP")
    |]
    let res = testData |> Array.filter (fun i -> validator.Validate(i).IsValid)
    Assert.Equal(2, res.Length)
