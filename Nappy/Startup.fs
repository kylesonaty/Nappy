namespace Nappy

open System
open Microsoft.Owin
open Owin

type Startup () =
    member this.Configuration(app : IAppBuilder) = 
        app.Run(fun context ->
            Pipeline.Run context
        )
        Initializer.RegisterRoutes()