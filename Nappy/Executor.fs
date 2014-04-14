namespace Nappy

open System
open Microsoft.Owin
open Newtonsoft.Json

module Executor =
    let Run(context:IOwinContext, nappy:INappyModule) =
        nappy.Before(context)
        let actionDescription = new ActionDescription(context)
        let result = nappy.Execute(actionDescription)
        let s = JsonConvert.SerializeObject(result) // add serialization based on accepts header (json, xml, form?)
        nappy.After(context)
        (context, s)