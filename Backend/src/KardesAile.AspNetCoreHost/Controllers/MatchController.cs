using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Error;
using KardesAile.CommonTypes.ViewModels.Match;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class MatchController: ControllerBase
{
    private readonly IMatchingBusiness _matchingBusiness;

    public MatchController(IMatchingBusiness matchingBusiness)
    {
        _matchingBusiness = matchingBusiness ?? throw new ArgumentNullException(nameof(matchingBusiness));
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return Ok(await _matchingBusiness.Get(id));
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Create([FromBody] CreateMatchModel model)
    {
        await _matchingBusiness.Create(model);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Update([FromBody] UpdateMatchModel model)
    {
        await _matchingBusiness.Update(model);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _matchingBusiness.Delete(id);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultModel<MatchResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Search([FromBody] SearchMatchModel model)
    {
        return Ok(await _matchingBusiness.Search(model));
    }
}