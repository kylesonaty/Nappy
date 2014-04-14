namespace Nappy.Demo

open System
open Nappy

type Product = { name: string; price: decimal }

[<RouteAttribute("products")>]
type ProductModule() = 
    inherit NappyModule<Product>()
    override this.Get() =
        let product1 = { name = "A"; price = 3.99m}
        let product2 = { name = "B"; price = 5.99m}
        let product3 = { name = "C"; price = 7.99m}
        [|product1; product2; product3|] |> Seq.cast
    override this.Get(id) =
        let product = { name = "A"; price = 3.99m}
        product
        
    // need to implement post, put, and delete