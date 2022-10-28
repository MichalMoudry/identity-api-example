module AssertFunctions

open Xunit

let equal (expected: 'a) (actual: 'a) =
    Assert.Equal<'a>(expected, actual)

let notNullOrEmpty (value: string) =
    Assert.NotNull(value)
    Assert.NotEqual<string>("", value)
