using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Audit;
using KardesAile.CommonTypes.ViewModels.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class AuditController : ControllerBase
{
    private readonly IAuditBusiness _auditBusiness;

    public AuditController(IAuditBusiness auditBusiness)
    {
        _auditBusiness = auditBusiness ?? throw new ArgumentNullException(nameof(auditBusiness));
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuditDetailResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> GetAuditDetails([FromQuery] Guid auditId)
    {
        return Ok(await _auditBusiness.GetAuditDetails(auditId));
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuditEffectedUserResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> GetAuditEffectedUsers([FromQuery] Guid auditId)
    {
        return Ok(await _auditBusiness.GetAuditEffectedUsers(auditId));
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultModel<AuditSearchResultModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Search([FromBody] SearchAuditModel model)
    {
        return Ok(await _auditBusiness.Search(model));
    }
}