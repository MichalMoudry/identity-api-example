namespace IdentityApi
#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)
        let dbConnectionString = 
            match builder.Environment.IsDevelopment() with
            | true -> builder.Configuration["ConnectionString"]
            | false -> ""

        builder.Services.AddControllers()

        let app = builder.Build()

        (*
        if app.Environment.IsDevelopment() then
            app.UseHttpsRedirection()
            app.UseHsts()
        else
            //app.UseSw()
        *)

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode