namespace Nappy

open System
open System.Dynamic
open Microsoft.Owin

module Responder = 
    let Run (context:IOwinContext, data:string) =
        data |> context.Response.WriteAsync