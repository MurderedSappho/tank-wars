namespace API

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open API.Hubs
open Newtonsoft.Json.Serialization
open Infrastructure
open TankWar.Storage.Repository
open TankWar.Model.User
open TankWar.Core.User

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration
        
    member this.ConfigureServices(services: IServiceCollection) =
        services
            .AddSignalR()
            .AddJsonProtocol(fun options -> 
                options.PayloadSerializerSettings.ContractResolver <- new DefaultContractResolver())
                |> ignore

        services
            .AddCors()
            |> ignore
        
        services
            .AddTransient<IIdentityContext, IdentityContext>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IRepository<UserData>, MongoRepository<UserData>>()
            |> ignore
        

        services
            .AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1) 
            |> ignore
        
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseCors(fun builder ->
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials() 
                |> ignore) 
                    |> ignore

        app.UseSignalR(fun routes ->
            routes.MapHub<GameHub>(new Microsoft.AspNetCore.Http.PathString("/game-hub")))|> ignore 

        

        app.UseMvc() |> ignore

    member val Configuration : IConfiguration = null with get, set