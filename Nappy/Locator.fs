namespace Nappy

open System
open System.Collections.Generic
open Microsoft.Owin

module Locator =
    let Routes = new Dictionary<string, INappyModule>() :> IDictionary<string, INappyModule>

    let getResourceFromRequest (request:IOwinRequest) = 
        request.Uri.LocalPath.Split('/') |> Seq.skip(1) |> Seq.head

    let getInstanceFromResource (resource:string) =
        match Routes.ContainsKey(resource) with
        | true -> Routes.[resource]
        | false -> failwith "No route found!"

    let Run (context: IOwinContext) =
        let instance = context.Request |> getResourceFromRequest |> getInstanceFromResource
        (context, instance)