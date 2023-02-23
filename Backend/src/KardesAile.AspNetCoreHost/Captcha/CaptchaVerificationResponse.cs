using System.Text.Json.Serialization;

namespace KardesAile.AspNetCoreHost.Captcha;

public class CaptchaVerificationResponse
{
    public bool Success { get; set; }
    [JsonPropertyName("challenge_ts")]
    public DateTime ChallengeTimestamp { get; set; }
    public string Hostname { get; set; }
    [JsonPropertyName("error-codes")]
    public string[] ErrorCodes { get; set; }
}