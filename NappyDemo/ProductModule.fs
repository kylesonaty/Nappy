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
    override this.Post(product) = 
        printfn "Product -> Name: %A Price: %A" product.name product.price
        "the url of the new product"
    override this.Post(id, product) =
        printfn "Product -> ID: %A Name: %A Price: %A" id product.name product.price

    // need to implement put and delete