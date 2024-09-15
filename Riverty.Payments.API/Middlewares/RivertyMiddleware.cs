namespace HK.Payments.API.Middlewares;

using HK.Payments.Core.Consts;
using HK.Payments.Core.Exceptions;
using HK.Payments.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class RivertyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RivertyMiddleware> _logger;

    public RivertyMiddleware(RequestDelegate next, ILogger<RivertyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (context?.Response != null && ex != null)
                await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        int code = (int)HttpStatusCode.InternalServerError;

        var messageModel = new ErrorResponse();

        string msg = string.IsNullOrWhiteSpace(ex.Message) ? "Internal error occurred" : ex.Message;

        if (msg.StartsWith("One or more errors occurred.") && ex.InnerException != null)
        {
            msg = ex.InnerException.Message;
        }

        if (ex is ValidationException || ex.InnerException is ValidationException)
        {
            code = (int)HttpStatusCode.UnprocessableEntity;
        }

        if (ex is DomainException)
        {
            code = (int)HttpStatusCode.UnprocessableEntity;
            var domainException = ex as DomainException;
            messageModel.Errors = domainException.ExceptionsMessages;
        }

        if (ex is UnauthorizedAccessException || ex.InnerException is UnauthorizedAccessException)
        {
            code = (int)HttpStatusCode.Unauthorized;
        }

        if (ex is BadHttpRequestException || ex.InnerException is BadHttpRequestException)
        {
            code = (int)HttpStatusCode.BadRequest;
        }

        var internalErrorCode = Guid.NewGuid().ToString().ToUpper();

        messageModel.Code = code;
        messageModel.Reason = msg;
        messageModel.Detail = ex.InnerException?.ToString();
        messageModel.InternalErrorCode = internalErrorCode;

        string result = JsonSerializer.Serialize(messageModel);

        _logger.LogError($"Error code: {internalErrorCode}");
        _logger.LogError($"Stack Trace: {ex.StackTrace}");

        httpContext.Response.ContentType = Common.APPLICATION_JSON;
        httpContext.Response.StatusCode = code;

        return httpContext.Response.WriteAsync(result);
    }
}