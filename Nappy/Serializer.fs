namespace Nappy

open System

open System.IO
open System.Text
open System.Runtime.Serialization
open System.Xml
open Microsoft.Owin
open Newtonsoft.Json

module Serializer = 
    
    let xmlSerialize (data:obj) = 
        let serializer = new DataContractSerializer(t)
        let stringWriter = new StringWriter()
        let xmlWriter = new XmlTextWriter(stringWriter)
        serializer.WriteObject(xmlWriter, data)
        stringWriter.ToString()

    let Serialize (context:IOwinContext, data:obj) =
        let contentType = context.Request.Accept |> ContentTypes.GetContentType
        match contentType with 
        | ContentType.Json -> JsonConvert.SerializeObject data
        | ContentType.Xml -> xmlSerialize data
        | ContentType.Form -> failwith "Not Implemented"
        | _ -> JsonConvert.SerializeObject data