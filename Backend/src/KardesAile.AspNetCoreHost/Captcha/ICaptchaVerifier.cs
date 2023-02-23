namespace KardesAile.AspNetCoreHost.Captcha;

public interface ICaptchaVerifier
{
    Task<bool> Verify(string token);
}