using Ngx.Monorepo.Framework.Extensions.Middleware;
using System;
using System.Security.Claims;

namespace Ngx.Monorepo.Framework.Extensions.Exceptions
{
    /// <summary>
    /// Exception for missing identity user claims in a request.
    /// </summary>
    public class MissingIdentityUserClaimException : Exception
    {
        public MissingIdentityUserClaimException(string claim)
            : base($"Missing {nameof(Claim)} in JWT needed for {nameof(IdentityUserClaimsForwardingMiddleware)}. Missing {nameof(Claim)}: {claim}.")
        { }
    }
}
