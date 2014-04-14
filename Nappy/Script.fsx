open System
open System.IO
open System.Net

let url = "http://localhost:8081/products"

let get(url:string) = 
    async {
        try 
            let uri = new Uri(url)
            let client = WebRequest.CreateHttp(uri)
            client.Method <- "GET"
            let! asyncResponse = client.AsyncGetResponse() 
            let response = asyncResponse :?> HttpWebResponse
            let stream = response.GetResponseStream()
            let reader = new StreamReader(stream)
            let json = reader.ReadToEnd()
            printfn "%s -> %s" response.StatusDescription json
            
        with 
            | ex -> printfn "%s" ex.Message
    }

url |> get |> Async.RunSynchronously |> ignore
[1 .. 10000] |> Seq.map(fun _-> url) |> Seq.map(fun x -> get x) |> Seq.iter(fun x-> x |> Async.RunSynchronously) |> ignore
//[1 .. 10000] |> Seq.map(fun _-> url) |> Seq.map(fun x -> get x) |> Async.Parallel |> Async.Ignore |> Async.Start