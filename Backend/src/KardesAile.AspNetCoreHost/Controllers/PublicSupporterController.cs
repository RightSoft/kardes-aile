using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.ViewModels.Error;
using KardesAile.CommonTypes.ViewModels.Supporter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class PublicSupporterController : ControllerBase
{
    private readonly ISupporterBusiness _supporterBusiness;

    public PublicSupporterController(ISupporterBusiness supporterBusiness)
    {
        _supporterBusiness = supporterBusiness ?? throw new ArgumentNullException(nameof(supporterBusiness));
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Create([FromBody] CreateSupporterModel model)
    {
        await _supporterBusiness.Create(model, true);
        return Ok();
    }
}