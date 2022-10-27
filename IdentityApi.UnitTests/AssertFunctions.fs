module AssertFunctions

open Xunit

let Equal (expected: 'a) (actual: 'a) =
    Assert.Equal<'a>(expected, actual)

let NotNullOrEmpty (value: string) =
    Assert.NotNull(value)
    Assert.NotEqual<string>("", value)
