namespace TankWar.Model

module User =
    open System

    [<Flags>]
    type UserState =
        | Offline = 0
        | Online = 1
        | InGame = 2
        | InLobby = 4

    let searchingForGame = 
        (UserState.Online ||| UserState.InLobby)

    type UserId = UserId of int
    type UserName = UserName of string


    type UserData = 
        { Id: UserId
          Name: UserName
          State: UserState }

    type UserCreateData = 
        { Name: UserName }