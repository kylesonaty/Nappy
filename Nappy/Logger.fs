namespace Nappy

open System
open System.Diagnostics
open Microsoft.Owin
open NLog

module Logger =
    // implement W3C log format
    let RecordRequest (context:IOwinContext) = 
        let logger = NLog.LogManager.GetCurrentClassLogger()
        let time = DateTime.UtcNow.ToString("HH:mm:ss")
        logger.Info(String.Format("{0} {1} {2}", time, context.Request.Method, context.Request.Uri.LocalPath))