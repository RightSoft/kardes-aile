using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.DisasterVictim;
using KardesAile.CommonTypes.ViewModels.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class DisasterVictimController : ControllerBase
{
    private readonly IDisasterVictimBusiness _disasterVictimBusiness;

    public DisasterVictimController(IDisasterVictimBusiness disasterVictimBusiness)
    {
        _disasterVictimBusiness = disasterVictimBusiness ?? throw new ArgumentNullException(nameof(disasterVictimBusiness));
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return Ok(await _disasterVictimBusiness.Get(id));
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Create([FromBody] CreateDisasterVictimModel model)
    {
        await _disasterVictimBusiness.Create(model);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Update([FromBody] UpdateDisasterVictimModel model)
    {
        await _disasterVictimBusiness.Update(model);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _disasterVictimBusiness.Delete(id);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultModel<DisasterVictimSearchResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Search([FromBody] SearchDisasterVictimModel model)
    {
        return Ok(await _disasterVictimBusiness.Search(model));
    }
}