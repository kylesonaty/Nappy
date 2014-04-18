namespace Nappy

open System
open System.IO
open System.Text
open System.Runtime.Serialization
open System.Xml
open Newtonsoft.Json

module Binder =
    let DeserializeJson(t:Type, content:string) =
        JsonConvert.DeserializeObject(content,t)

    let DeserializeXml(t:Type, content:string) =
        let serializer = new DataContractSerializer(t)
        let reader = new XmlTextReader(new StringReader(content))
        serializer.ReadObject(reader)

    let Bind (t:Type, content:string, contentType:ContentType) =
        match content with
        | c when c |> String.IsNullOrEmpty -> new obj()
        | c -> 
            match contentType with
                | ContentType.Json -> DeserializeJson(t, content)
                | ContentType.Xml  -> DeserializeXml(t, content)
                | ContentType.Form -> failwith "Not Implemented"
                | ContentType.None -> failwith "Not Implemented"