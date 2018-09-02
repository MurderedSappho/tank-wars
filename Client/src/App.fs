module Client

open Fable.Core.JsInterop
open Fable.Import
open Fable.Core
open Fable.PowerPack

type [<AllowNullLiteral>] HubConnection =
    abstract connection: obj
    abstract logger: obj
    abstract protocol: obj with get, set
    abstract handshakeProtocol: obj with get, set
    abstract callbacks: obj with get, set
    abstract methods: obj with get, set
    abstract id: obj with get, set
    abstract closedCallbacks: obj with get, set
    abstract timeoutHandle: obj with get, set
    abstract receivedHandshakeResponse: obj with get, set
    abstract serverTimeoutInMilliseconds: float with get, set
    abstract start: unit -> JS.Promise<_>
    abstract stop: unit -> unit
    abstract on: methodName: string * newMethod: (ResizeArray<obj option> -> unit) -> unit
    abstract off: methodName: string -> unit
    abstract off: methodName: string * ``method``: (ResizeArray<obj option> -> unit) -> unit
    abstract processIncomingData: data: obj -> unit
    abstract processHandshakeResponse: data: obj -> unit
    abstract configureTimeout: unit -> unit
    abstract serverTimeout: unit -> unit
    abstract invoke: method: string * message: string -> unit
    abstract invokeClientMethod: invocationMessage: obj -> unit
    abstract connectionClosed: ?error: obj -> unit
    abstract cleanupTimeout: unit -> unit
    abstract createInvocation: methodName: obj * args: obj * nonblocking: obj -> unit
    abstract createStreamInvocation: methodName: obj * args: obj -> unit
    abstract createCancelInvocation: id: obj -> unit

type [<AllowNullLiteral>] HubConnectionBuilder =
    abstract withUrl: url: string -> HubConnectionBuilder
    abstract build: unit -> HubConnection

[<Import("HubConnectionBuilder", from="@aspnet\signalr")>]
[<Emit("new HubConnectionBuilder()")>]
let hubConnectionBuilder : HubConnectionBuilder = jsNative

let connection = hubConnectionBuilder.withUrl("http://localhost:20000/game-hub").build()

let onStart() = promise {
    // Add here all subscriptions you need
    Browser.console.log("started")
}

connection
    .start()
    |> Promise.bind onStart
    |> ignore

let init() =
    
    let canvas = Browser.document.getElementsByTagName_canvas().[0]
    
    canvas.width <- 1000.

    canvas.height <- 800.
    let ctx = canvas.getContext_2d()
    // The (!^) operator checks and casts a value to an Erased Union type
    // See http://fable.io/docs/interacting.html#Erase-attribute
    ctx.fillStyle <- !^"rgb(200,0,0)"
    ctx.fillRect (10., 10., 55., 50.)
    ctx.fillStyle <- !^"rgba(0, 0, 200, 0.5)"
    ctx.fillRect (30., 30., 55., 50.)

init()

//Browser.window.setTimeout((fun() -> connection.invoke("sendMessage", "ololol")), 1000, new obj()) |> ignore