using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KardesAile.AspNetCoreHost.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class AddressController : ControllerBase
{
    private readonly IAddressBusiness _addressBusiness;

    public AddressController(IAddressBusiness addressBusiness)
    {
        _addressBusiness = addressBusiness ?? throw new ArgumentNullException(nameof(addressBusiness));
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRoles.User))]
    public async Task<IActionResult> GetCountries()
    {
        return Ok(await _addressBusiness.GetCountries());
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRoles.User))]
    public async Task<IActionResult> GetCities([FromQuery] Guid countryId)
    {
        return Ok(await _addressBusiness.GetCities(countryId));
    }
}