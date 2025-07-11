using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESG.Common.Web.Utility.Extensions;

/// <summary>
/// A class containing common properties and methods used internally by the library.
/// </summary>
public static class Common
{
    internal static IActionResult Ok<T>(T t) => t is Unit ? NoContent() : OkResult(t);

    internal static IActionResult NotFound() => new NotFoundResult();

    internal static IActionResult BadRequest<ERROR>(ERROR e) => new BadRequestObjectResult(e);

    internal static IActionResult BadRequest() => new BadRequestResult();

    internal static IActionResult ServerError<E>(E e) => Error(e);

    internal static IActionResult ServerError() => Error();

    internal static IActionResult NoContent() => new NoContentResult();

    internal static JsonResult OkJson<T>(T t) => OkJsonResult(t);

    internal static JsonResult BadRequestJson<E>(E e) => BadRequestJsonResult(e);

    internal static IActionResult ServerErrorWithLogging(Exception e, Action<Exception>? fail)
    {
        if (fail != null)
        {
            fail(e);
            return ServerError();
        }
        return ServerError(e);
    }

    private static OkObjectResult OkResult<T>(T t)
    {
        return new OkObjectResult(t);
    }

    static JsonResult OkJsonResult<T>(T t) =>
        new(t) { StatusCode = StatusCodes.Status200OK };

    static JsonResult BadRequestJsonResult<E>(E e) =>
        new(e) { StatusCode = StatusCodes.Status400BadRequest };

    private static StatusCodeResult Error()
    {
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    private static ObjectResult Error<E>(E e) =>
        new(e) { StatusCode = StatusCodes.Status500InternalServerError };
}