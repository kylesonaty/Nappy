namespace Nappy

open System
open Microsoft.Owin
open Owin

module Pipeline =
    let Run (context: IOwinContext) =
        try
            Logger.RecordRequest context
            context
                |> Locator.Run
                |> Executor.Run
                |> Responder.Run
        with
        | ex -> Error.Handle(context, ex)