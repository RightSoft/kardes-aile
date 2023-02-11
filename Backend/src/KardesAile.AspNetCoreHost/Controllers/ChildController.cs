using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels.Child;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class ChildController : ControllerBase
{
    private readonly IChildBusiness _childBusiness;

    public ChildController(IChildBusiness childBusiness)
    {
        _childBusiness = childBusiness ?? throw new ArgumentNullException(nameof(childBusiness));
    }
    
    [HttpGet("{userId:guid:required}")]
    [Authorize(Roles = nameof(UserRoles.User))]
    public async Task<IActionResult> List([FromRoute] Guid userId)
    {
        return Ok(await _childBusiness.List(userId));
    }

    [HttpPost("{userId:guid:required}")]
    [Authorize(Roles = nameof(UserRoles.User))]
    public async Task<IActionResult> Add([FromRoute] Guid userId, [FromBody] CreateChildModel model)
    {
        await _childBusiness.Add(userId, model);
        return Ok();
    }

    [HttpDelete]
    [Authorize(Roles = nameof(UserRoles.User))]
    public async Task<IActionResult> Remove([FromQuery] Guid id)
    {
        await _childBusiness.Remove(id);
        return Ok();
    }
}