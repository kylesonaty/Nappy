namespace Nappy

open System
open System.Diagnostics
open Microsoft.Owin

module Logger =
    // implement W3C log format
    let RecordRequest (context:IOwinContext) = 
        let time = DateTime.UtcNow.ToString("HH:mm:ss")
        System.Console.WriteLine("{0} {1} {2}",time, context.Request.Method, context.Request.Uri.LocalPath)