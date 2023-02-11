using System.Security;
using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.ViewModels.Authentication;
using KardesAile.CommonTypes.ViewModels.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationBusiness _authenticationBusiness;

    public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
    {
        _authenticationBusiness =
            authenticationBusiness ?? throw new ArgumentNullException(nameof(authenticationBusiness));
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResultModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel model)
    {
        try
        {
            var result = await _authenticationBusiness.Authenticate(model);
            return Ok(result);
        }
        catch (SecurityException e)
        {
            return Unauthorized(e.Message);
        }
    }
}