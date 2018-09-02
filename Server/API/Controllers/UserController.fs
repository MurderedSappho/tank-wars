namespace API.Controllers

open Microsoft.AspNetCore.Mvc
open TankWar.Core.User
open API.Infrastructure


[<Route("api/[controller]")>]
[<ApiController>]
type UserController (_identityContext: IIdentityContext,
                     _userService: IUserService) =
    
    inherit ControllerBase()

    [<HttpGet("me")>]
    member this.Get() =
        _identityContext.GetIdentity(this.HttpContext)

    [<HttpGet()>]
    member this.GetAllAsync() =
        _userService.GetAsync()

    [<HttpPost>]
    member this.SignUp() =
        _identityContext.GetIdentity(this.HttpContext)