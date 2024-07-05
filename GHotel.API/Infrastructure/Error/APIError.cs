using System.Net;
using GHotel.API.Infrastructure.Localization;
using GHotel.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GHotel.API.Infrastructure.Error;

public class APIError : ProblemDetails
{
    private const string UnhandledErrorCode = "UnhandledErrorCode";
    private readonly HttpContext _httpContext;
    private readonly Exception _exception;

    public string Code { get; set; }
    public string? TraceId
    {
        get
        {
            if (Extensions.TryGetValue("TraceId", out var traceId))
                return (string?)traceId;
            return null;
        }
        set => Extensions["TraceId"] = value;
    }

    public APIError(Exception exception, HttpContext httpContext)
    {
        _httpContext = httpContext;
        _exception = exception;
        Status = (int)HttpStatusCode.InternalServerError;
        Code = UnhandledErrorCode;
        TraceId = httpContext.TraceIdentifier;
        Title = ErrorMessages.GlobalExceptionTitle;
        Instance = httpContext.Request.Path;
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";

        HandleException((dynamic)exception);
    }

    public void HandleException(Exception exception)
    {
    }

    public void HandleException(NotFoundException exception)
    {
        Status = (int)HttpStatusCode.NotFound;
        Code = exception.Code;
        Title = "Resource Is Not Found";
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
    }

}
