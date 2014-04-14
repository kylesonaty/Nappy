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