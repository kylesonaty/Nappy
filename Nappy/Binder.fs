namespace Nappy

open System
open Newtonsoft.Json

module Binder =
    let DeserializeJson(t:Type, content:string) =
        JsonConvert.DeserializeObject(content,t)

    let Bind (t:Type, content:string, contentType:ContentType) =
        match content with
        | c when c |> String.IsNullOrEmpty -> new obj()
        | c -> 
            match contentType with
                | ContentType.Json -> DeserializeJson(t, content)
                | ContentType.Xml  -> failwith "Not Implemented"
                | ContentType.Form -> failwith "Not Implemented"
                | ContentType.None -> failwith "Not Implemented"