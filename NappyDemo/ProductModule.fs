namespace Nappy.Demo

open System
open Nappy

type Product = { name: string; price: decimal }

[<RouteAttribute("products")>]
type ProductModule() = 
    inherit NappyModule<Product>()
    override this.Get() =
        printfn "This should get a list of URIs of items in the collection"
        [|"1"; "2"; "3"|] |> Seq.cast
        
    override this.Get(id) =
        printfn "This should get the product at: %A" id
        let product = { name = "A"; price = 3.99m}
        product

    override this.Post(product) = 
        printfn "This should create a new product and return the location of the new product"
        printfn "Product -> Name: %A Price: %A" product.name product.price
        "the url of the new product"

    override this.Post(id, product) =
        printfn "This should treat this item as a collection and append to the item at collection: %A" id
        printfn "Product -> ID: %A Name: %A Price: %A" id product.name product.price

    override this.Delete() =
        printfn "This should delete everything"

    override this.Delete(id) =
        printfn "This should delete ID: %A" id

    override this.Put(products) =
        printfn "This should replace the entire collection"

    override this.Put(id, products) =
        printfn "This should replace the collection at: %A or create it" id