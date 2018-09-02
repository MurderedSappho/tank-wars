namespace TankWar.Storage

module Repository =
    open MongoDB.Driver

    type IRepository<'T> =
        abstract member GetAsync<'T> : unit -> Async<list<'T>>

    type MongoRepository<'T> () =
        interface IRepository<'T> with
            member this.GetAsync(): Async<list<'T>> =
                let client = new MongoClient("mongodb://localhost:27017")
                let database = client.GetDatabase("tank_wars")
                let collection = database.GetCollection<'T>("users")
                
                async {
                    let! cursor = collection
                                    .FindAsync(fun _ -> true) 
                                    |> Async.AwaitTask

                    let! items = cursor
                                    .ToListAsync()
                                    |> Async.AwaitTask

                    return List.ofSeq items
                }
