using System.Security;
using KardesAile.CommonTypes.Exceptions;
using KardesAile.CommonTypes.ViewModels.Error;
using Microsoft.AspNetCore.Diagnostics;

namespace KardesAile.AspNetCoreHost.Middlewares;

public class GlobalExceptionManager
{
    public static async Task Handler(HttpContext context)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<GlobalExceptionManager>>();

        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
            switch (contextFeature.Error)
            {
                case BusinessException businessException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(
                        new ErrorModel
                        {
                            Code = businessException.Code,
                            Error = businessException.Message,
                            StatusCode = StatusCodes.Status400BadRequest
                        });
                    return;
                case SecurityException securityException:
                    logger.LogWarning(contextFeature.Error, "Security problem on path: {Path}", contextFeature.Path);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(new ErrorModel
                    {
                        Code = 403,
                        Error = securityException.Message,
                        StatusCode = StatusCodes.Status403Forbidden
                    });
                    break;
                default:
                    logger.LogError(contextFeature.Error, "Unhandled error on path: {Path}", contextFeature.Path);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new ErrorModel
                    {
                        Code = 500,
                        Error = "Something went wrong. We are trying to fix the issue. Please try again later.",
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
                    break;
            }
    }
}