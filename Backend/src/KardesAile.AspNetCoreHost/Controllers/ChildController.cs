using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels.Child;
using KardesAile.CommonTypes.ViewModels.Error;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChildResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> List([FromRoute] Guid userId)
    {
        return Ok(await _childBusiness.List(userId));
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.User))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Add([FromBody] CreateChildModel model)
    {
        await _childBusiness.Add(model);
        return Ok();
    }
    
    [HttpPut]
    [Authorize(Roles = nameof(UserRoles.User))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Update([FromBody] UpdateChildModel model)
    {
        await _childBusiness.Update(model);
        return Ok();
    }

    [HttpDelete("{id:guid:required}")]
    [Authorize(Roles = nameof(UserRoles.User))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        await _childBusiness.Remove(id);
        return Ok();
    }
}