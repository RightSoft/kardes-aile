using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels.DisasterVictim;
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

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Create([FromBody] CreateDisasterVictimModel model)
    {
        await _disasterVictimBusiness.Create(model);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Update([FromBody] UpdateDisasterVictimModel model)
    {
        await _disasterVictimBusiness.Update(model);
        return Ok();
    }
    
    [HttpDelete]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await _disasterVictimBusiness.Delete(id);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
    public async Task<IActionResult> Search([FromBody] SearchDisasterVictimModel model)
    {
        return Ok(await _disasterVictimBusiness.Search(model));
    }
}