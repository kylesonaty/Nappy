namespace Nappy

open System
open System.Collections.Generic
open System.Dynamic
open Microsoft.Owin

[<Interface>]
type INappyModule = 
    abstract member Before: IOwinContext -> unit
    abstract member After: IOwinContext -> unit
    abstract member Error: IOwinContext * exn -> unit
    abstract member Execute: ActionDescription -> obj

[<AbstractClass>]
type NappyModule<'a> () =
    interface INappyModule with 
        member this.Before(context) = context |> ignore
        member this.After(context) = context |> ignore
        member this.Error(context, exn) = context |> ignore
        member this.Execute(description) = 
            let list = Seq.empty<'a> // need to get this from the body and model bind
            let item = Unchecked.defaultof<'a> // need to get this from the body and model bind

            match description.Identifier with
            | id when id |> String.IsNullOrEmpty -> 
                match description.Method with
                    | HttpMethod.Get -> this.Get() :> obj
                    | HttpMethod.Put -> this.Put(list) :> obj 
                    | HttpMethod.Post -> this.Post(item) :> obj
                    | HttpMethod.Delete -> this.Delete() :> obj
            | id ->
                match description.Method with
                    | HttpMethod.Get -> this.Get(id) :> obj
                    | HttpMethod.Put ->this.Put(id, list) :> obj 
                    | HttpMethod.Post -> this.Post(id, item) :> obj
                    | HttpMethod.Delete -> this.Delete(id) :> obj

    abstract member Get: unit -> IEnumerable<'a>
    abstract member Get: string -> 'a

    abstract member Post: 'a-> unit
    abstract member Post: string * 'a -> unit 

    abstract member Put: IEnumerable<'a> -> unit
    abstract member Put: string * IEnumerable<'a> -> unit

    abstract member Delete: unit -> unit
    abstract member Delete: string -> unit

    default this.Get() = failwith "Method not implemented"
    default this.Get(x : string) = failwith "Method not implemented" 
    
    default this.Post(x:'a) = failwith "Method not implemented"
    default this.Post(x:string, y:'a) = failwith "Method not implemented"

    default this.Put(x:IEnumerable<'a>) = failwith "Method not implemented"
    default this.Put(x:string, y:IEnumerable<'a>) = failwith "Method not implemented" 
    
    default this.Delete() = failwith "Method not implemented"
    default this.Delete(x:string) = failwith "Method not implemented"