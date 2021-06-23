using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ngx.Monorepo.Framework.Extensions.Controllers
{
    /// <summary>
    /// BaseController for microservices.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public abstract class BaseController : ControllerBase
    {
        // <summary>
        /// Requesting Product Id from the header populated by the ProductAuthorizationMiddleware.
        /// </summary>
        protected Guid RequestingProductId => string.IsNullOrEmpty(Request.Headers["Requesting-ProductId"])
            ? Guid.Empty
            : new Guid(Request.Headers["Requesting-ProductId"]);

        /// <summary>
        /// Requesting Product Name from the header populated by ProductAuthorizationMiddleware.
        /// </summary>
        protected string RequestingProductName => string.IsNullOrEmpty(Request.Headers["Requesting-Product"])
            ? string.Empty
            : Request.Headers["Requesting-Product"].ToString();

        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
