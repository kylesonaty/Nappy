namespace Nappy

open System
open Microsoft.Owin

module Error =
    // perhaps a better error page
    let Handle(context:IOwinContext, error:Exception) = 
        context.Response.StatusCode <- 500
        "An Error Occured " + error.Message |> context.Response.WriteAsync 