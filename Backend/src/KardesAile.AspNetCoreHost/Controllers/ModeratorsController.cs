using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels.Moderators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[Authorize(Roles = nameof(UserRoles.GlobalAdmin))]
public class ModeratorsController : KardesAileBaseController
{
	private readonly IModeratorBusiness _moderatorBusiness;

	public ModeratorsController(
        IModeratorBusiness moderatorBusiness)
	{
		_moderatorBusiness = moderatorBusiness
			?? throw new ArgumentNullException(nameof(moderatorBusiness));
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] CreateModeratorModel model)
	{
		await _moderatorBusiness.CreateAsync(model);

		return Ok();
	}

    [HttpGet("{id}")]
    public async Task<IActionResult> ReadAsync([FromRoute] Guid id)
    {
        var result = await _moderatorBusiness.ReadAsync(id);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateModeratorModel model)
    {
        await _moderatorBusiness.UpdateAsync(id, model);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _moderatorBusiness.DeleteAsync(id);

        return Ok();
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchAsync([FromBody] SearchModeratorModel model)
    {
        var result = await _moderatorBusiness.SearchAsync(model);

        return Ok(result);
    }
}
