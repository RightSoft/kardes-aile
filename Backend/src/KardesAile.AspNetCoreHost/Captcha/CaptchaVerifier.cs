using KardesAile.CommonTypes.Options;
using Microsoft.Extensions.Options;

namespace KardesAile.AspNetCoreHost.Captcha;

public class CaptchaVerifier : ICaptchaVerifier
{
    private const string GoogleVerificationUrl = "https://www.google.com/recaptcha/api/siteverify";
    private readonly ILogger<CaptchaVerifier> _logger;
    private readonly IOptions<CaptchaOptions> _captchaOptions;
    private readonly HttpClient _httpClient;

    public CaptchaVerifier(
        ILogger<CaptchaVerifier> logger,
        IOptions<CaptchaOptions> captchaOptions,
        HttpClient httpClient
    )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _captchaOptions = captchaOptions ?? throw new ArgumentNullException(nameof(captchaOptions));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public async Task<bool> Verify(string token)
    {
        try
        {
            var response = await _httpClient.PostAsync($"{GoogleVerificationUrl}?secret={_captchaOptions.Value.ServerKey}&response={token}", null);
            response.EnsureSuccessStatusCode();
            var verificationResult = await response.Content.ReadFromJsonAsync<CaptchaVerificationResponse>();

            return verificationResult.Success;
        }
        catch (Exception e)
        {
            // fail gracefully, but log
            _logger.LogError("Failed to process captcha validation", e);
        }

        return false;
    }
}