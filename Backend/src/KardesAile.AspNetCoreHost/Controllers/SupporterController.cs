using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
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

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Create([FromBody] CreateSupporterModel model)
    {
        await _supporterBusiness.Create(model);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Update([FromBody] UpdateSupporterModel model)
    {
        await _supporterBusiness.Update(model);
        return Ok();
    }
    
    [HttpDelete]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await _supporterBusiness.Delete(id);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Search([FromBody] SearchSupporterModel model)
    {
        return Ok(await _supporterBusiness.Search(model));
    }
}