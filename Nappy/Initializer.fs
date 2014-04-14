namespace Nappy

open System
open System.Linq
open System.Reflection
open Microsoft.Owin

module Initializer =
    let Run (context:IOwinContext) =
        context.Request.Uri.PathAndQuery

    let FindDerivedTypes(assembly:Assembly, baseType:Type) =
        assembly.GetTypes() |> Seq.filter(fun t -> baseType.IsAssignableFrom(t))

    let RegisterRoutes() =
        AppDomain.CurrentDomain.GetAssemblies()
            |> Seq.map(fun assembly -> FindDerivedTypes(assembly, typedefof<INappyModule>)) 
            |> Seq.concat
            |> Seq.filter(fun typeInfo -> typeInfo.CustomAttributes.Any(fun attributeData -> attributeData.AttributeType = typedefof<RouteAttribute>))
            |> Seq.map(fun typeInfo -> (typeInfo, typeInfo.GetCustomAttribute(typedefof<RouteAttribute>) :?> RouteAttribute))
            |> Seq.map(fun (nappy, route) -> (route.Route, Activator.CreateInstance(nappy) :?> INappyModule)) 
            |> Seq.iter(fun (key,value) -> Locator.Routes.Add(key,value))
