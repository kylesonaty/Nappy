namespace Nappy

type ContentType =
    | Xml
    | Json
    | Form
    | None

module ContentTypes =
    let GetContentType (contentType:string) =
        match contentType with
        | "application/json" -> ContentType.Json
        | "text/javascript" -> ContentType.Json
        | "text/xml" -> ContentType.Xml
        | "application/xhtml+xml" -> ContentType.Xml
        | "application/xml"-> ContentType.Xml
        | "application/x-www-form-urlencoded " -> ContentType.Form
        | _ -> ContentType.None