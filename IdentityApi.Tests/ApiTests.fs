module ApiTests

open Microsoft.AspNetCore.Mvc.Testing
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Storage
open Microsoft.Extensions.Hosting
open Xunit

(*
    type IdentityApi() =
    inherit WebApplicationFactory<Program>()

    override this.CreateHost(builder: IHostBuilder) =
        let root = new InMemoryDatabaseRoot()
        //builder.ConfigureServices()
        base.CreateHost(builder)
*)