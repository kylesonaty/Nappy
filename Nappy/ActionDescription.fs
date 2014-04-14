namespace Nappy

open System
open Microsoft.Owin

type ActionDescription (context:IOwinContext) = 
    let getIdentifierFromRequest (request:IOwinRequest) =
        let seq = request.Uri.LocalPath.Split('/') |> Seq.skip(2) 
        if Seq.isEmpty seq then
            ""
        else
            seq |> Seq.head

    let getHttpMethod (request:IOwinRequest) =
        let httpMethod = match request.Method with 
                            | "GET" -> HttpMethod.Get
                            | "POST" -> HttpMethod.Post
                            | "PUT" -> HttpMethod.Put
                            | "DELETE" -> HttpMethod.Delete
                            | _ -> failwith "Http Method not supported"
        httpMethod

    let getBody (request:IOwinRequest) = 
        let chars = 
            request.Body.AsyncRead(int request.Body.Length)
                |> Async.RunSynchronously 
                |> System.Text.Encoding.UTF8.GetChars 
        new String(chars)

    member this.Context = context
    member this.Identifier = this.Context.Request |> getIdentifierFromRequest
    member this.Method = this.Context.Request |> getHttpMethod
    member this.Body = this.Context.Request |> getBody