namespace Nappy

open System
open Microsoft.Owin
open Newtonsoft.Json

module Executor =
    let Run(context:IOwinContext, nappy:INappyModule) =
        nappy.Before(context)
        let actionDescription = new ActionDescription(context)
        let result = nappy.Execute(actionDescription)
        let serialized = Serializer.Serialize(context, result)
        nappy.After(context)
        (context, serialized)