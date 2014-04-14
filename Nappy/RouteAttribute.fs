namespace Nappy

open System
open System.Collections.Generic

type RouteAttribute(route:string) =
    inherit Attribute()
    member this.Route = route
