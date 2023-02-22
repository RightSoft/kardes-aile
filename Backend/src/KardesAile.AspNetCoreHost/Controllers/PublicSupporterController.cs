using KardesAile.AspNetCoreHost.Captcha;
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
    private readonly ICaptchaVerifier _captchaVerifier;

    public PublicSupporterController(ISupporterBusiness supporterBusiness, ICaptchaVerifier captchaVerifier)
    {
        _supporterBusiness = supporterBusiness ?? throw new ArgumentNullException(nameof(supporterBusiness));
        _captchaVerifier = captchaVerifier ?? throw new ArgumentNullException(nameof(captchaVerifier));
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<IActionResult> Create([FromBody] CreateSupporterByCaptchaModel model)
    {
        if (!await _captchaVerifier.Verify(model.ReCaptchaToken))
        {
            return BadRequest(new ErrorModel
            {
                Code = 400,
                Error = "Captcha doğrulama başarılı değil. Captcha yenileyerek tekrar deneyin.",
                StatusCode = 400
            });
        }

        await _supporterBusiness.Create(model, true);
        return Ok();
    }
}