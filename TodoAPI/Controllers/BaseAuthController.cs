using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers;

public class BaseAuthController : ControllerBase
{
    protected int GetUserId()
    {
        var claim = this.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.PrimarySid));

        if (claim == null)
        {
            throw new Exception("User not authorized");
        }

        var userId = int.Parse(claim.Value);

        return userId;
    }
}