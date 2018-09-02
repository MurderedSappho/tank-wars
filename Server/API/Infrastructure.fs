namespace API

module Infrastructure = 
    open TankWar.Model.User
    open Microsoft.AspNetCore.Http
    open Microsoft.Extensions.Primitives

    type IIdentityContext =
        abstract member GetIdentity: HttpContext -> Option<UserData>
    
    type IdentityContext () =
        let authorizationHeaderName = "Authorization"
        interface IIdentityContext with
            member this.GetIdentity(httpContext: HttpContext): Option<UserData> =
                let mutable headerValue = StringValues.Empty
                let isSuccess = 
                    httpContext
                        .Request
                        .Headers
                        .TryGetValue(authorizationHeaderName, &headerValue)

                if isSuccess then Some({ Id = 1 |> UserId; 
                                         Name = headerValue.ToString() |> UserName; 
                                         State = UserState.InGame })
                else None