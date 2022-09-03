module ValidationTests

open IdentityApi.Commands
open IdentityApi.Validators
open Xunit

[<Fact>]
let ``Register command validation`` () =
    let validator = new RegisterCommandValidator()
    let testData = [|
        new RegisterCommand(UserName = "Test username 1", Email = "test1@test.com", Password = "95UQTVQ!LTWtP")
        new RegisterCommand(UserName = "T2", Email = "test2@test.com", Password = "95UQTVQ!LTWtP")
        new RegisterCommand(UserName = "", Email = "test3@test.com", Password = "95UQTVQ!LTWtP")
        new RegisterCommand(UserName = "Test username 4", Email = "test4@test.com", Password = "95UQTVQ!LTWtP")
    |]
    let res = testData |> Array.filter (fun i -> validator.Validate(i).IsValid)
    Assert.Equal(2, res.Length)
