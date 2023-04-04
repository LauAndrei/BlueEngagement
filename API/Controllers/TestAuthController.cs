using API.Extensions.ClaimsExtensions;
using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAuthController : ControllerBase
{
    [HttpGet]
    [Route("TestHit")]
    public string TestHit()
    {
        return "Good endpoint";
    }
   
    [HttpGet]
    [Route("TestAuthBasic")]
    [Authorize]
    public string TestAuthBasic()
    {
        return "You should see this only if you are authenticated";
    }

    [HttpGet]
    [Route("TestAuthEmployee")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.EMPLOYEE)]
    public string TestAuthStandard()
    {
        return "You should see this only if you are authenticated as employee";
    }

    [HttpGet]
    [Route("TestAuthManager")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MANAGER)]
    public string TestAuthModerator()
    {
        return "You should see this only if you are authenticated as manager";
    }
   
    [HttpGet]
    [Route("GetLoggedInUserId")]
    public int GetLoggedInUserId()
    {
        return User.GetUserId();
    }

    [HttpGet]
    [Route("CheckIfUserIsAuth")]
    public bool CheckIfUserIsAuth()
    {
        return HttpContext.User.Identity.IsAuthenticated;
    }

    [HttpGet]
    [Route("GetLoggedInUserUsername")]
    public string GetLoggedInUserUsername()
    {
        return User.Identity.Name;
    }
}