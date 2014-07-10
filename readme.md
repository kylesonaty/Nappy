# Nappy #

Nappy is an opinionated REST framework for .NET. It is written in F# and uses the [Owin](http://owin.org/) web interfaces for .NET.


## Hosting ##

Here is an example of how to self host.

```fsharp
open System
open System.Linq
open System.Threading
open Microsoft.Owin.Hosting
open Nappy

[<EntryPoint>]
let main argv = 
    let url = "http://localhost:8081"
    WebApp.Start<Startup>(url) |> ignore
    Console.WriteLine("started server: {0}", url)
    match argv.Any(fun s -> s.Equals("-d", StringComparison.InvariantCultureIgnoreCase)) with
        | true -> Thread.Sleep Timeout.Infinite
        | _ -> Console.ReadLine() |> ignore
    0
```

## Creating a Module  ##

To create a module create a new class and inherit the NappyModule. Below is an example of a Product module:

```fsharp
open System
open Nappy

type Product = { name: string; price: decimal }

[<RouteAttribute("products")>]
type ProductModule() = 
    inherit NappyModule<Product>()
    override this.Get() =
        // return a list of products.
		let product1 = { name = "A"; price = 3.99m}
        let product2 = { name = "B"; price = 4.99m}
        let product3 = { name = "C"; price = 5.99m}
        [|product1;product2;product3|] |> Seq.cast
    override this.Get(id) =
		// return the product at the specified id
        let product = { name = "A"; price = 3.99m}
        product
```

The route attribute tells where your resource will be located. In this example `http://yourhostname/products` is where your module will be located. You can override the methods to implement whatever methods you want. For instance:

```fsharp
override this.Get()
override this.Get(id)
```
These allow you to handle:

`/product`

`/product/123`

## Nuget ##

Work in process.

## Things to do ##

- Implement W3C logging
- Better error page for diagnoistics 
- make more async