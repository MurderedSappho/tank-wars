namespace API.Hubs

open Microsoft.AspNetCore.SignalR

type GameHub () = 
    inherit Hub ()
    member this.SendMessage(message: string) =
        this.Clients.All.SendAsync("ReceiveMessage", message) |> ignore