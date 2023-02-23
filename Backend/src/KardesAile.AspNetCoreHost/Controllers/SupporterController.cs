using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Error;
using KardesAile.CommonTypes.ViewModels.Supporter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class SupporterController : ControllerBase
{
    private readonly ISupporterBusiness _supporterBusiness;

    public SupporterController(ISupporterBusiness supporterBusiness)
    {
        _supporterBusiness = supporterBusiness ?? throw new ArgumentNullException(nameof(supporterBusiness));
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return Ok(await _supporterBusiness.Get(id));
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Create([FromBody] CreateSupporterModel model)
    {
        await _supporterBusiness.Create(model, false);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Update([FromBody] UpdateSupporterModel model)
    {
        await _supporterBusiness.Update(model);
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
        await _supporterBusiness.Delete(id);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = $"{nameof(UserRoles.GlobalAdmin)},{nameof(UserRoles.Moderator)}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(PagedResultModel<SupporterSearchResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Search([FromBody] SearchSupporterModel model)
    {
        return Ok(await _supporterBusiness.Search(model));
    }
}