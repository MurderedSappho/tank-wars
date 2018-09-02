namespace TankWar.Core

module User =
    open TankWar.Model.User
    open TankWar.Storage.Repository

    type IUserService =
        abstract member CreateAsync: UserCreateData -> Async<Option<UserData>>
        abstract member GetAsync: unit -> Async<list<UserData>>
    
    type UserService (userRepository: IRepository<UserData>) =
        interface IUserService with
            
            member this.CreateAsync(userCreateData: UserCreateData): Async<Option<UserData>> =
                raise (System.NotImplementedException())

            member this.GetAsync(): Async<UserData list> = 
                userRepository.GetAsync()
