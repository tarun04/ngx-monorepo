using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ngx.Monorepo.Framework.Core.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Extensions.Middleware
{
    /// <summary>
    /// Middleware that handles exceptions.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            context.Response.ContentType = "application/json";
            if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                logger.LogError(ex, "Not found Exception: ");
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(ex.Message);
            }
            else if (ex is ValidationException exve)
            {
                code = HttpStatusCode.BadRequest;
                logger.LogError(ex, "Validation Exception: ");
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(string.Join(",", exve.Errors.Select(x => x)));
            }
            else if (ex is InvalidOperationException)
            {
                code = HttpStatusCode.Forbidden;
                logger.LogError(ex, "Invalid Operation Exception: ");
                context.Response.StatusCode = (int)code;
                context.Response.WriteAsync(ex.Message);
            }
            else
            {
                logger.LogCritical(ex, "Critial Error: ");
                code = HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)code;
            }
            return Task.CompletedTask;
        }
    }
}
