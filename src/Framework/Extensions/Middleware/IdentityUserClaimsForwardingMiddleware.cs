using IdentityModel;
using Microsoft.AspNetCore.Http;
using Ngx.Monorepo.Framework.Core.Extensions;
using Ngx.Monorepo.Framework.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Extensions.Middleware
{
    /// <summary>
    /// Middleware to be used in Gateways ONLY that takes a user's claims 
    /// required for <see cref="IdentityUser"/> and adds them as headers.
    /// These headers are then picked up by <see cref="CurrentIdentityUserMiddleware"/> in Microservices.
    /// This middleware must be registered before Ocelot!
    /// </summary>
    public class IdentityUserClaimsForwardingMiddleware
    {
        private readonly RequestDelegate next;
        // Dictionary of User Claims mapped to Identity User properties.
        private static readonly IReadOnlyDictionary<string, string> claimsToAdd = new Dictionary<string, string>
        {
            { JwtClaimTypes.Subject, nameof(IdentityUser.UserId) },
            { JwtClaimTypes.Email, nameof(IdentityUser.Email) },
            { JwtClaimTypes.GivenName, nameof(IdentityUser.GivenName) },
            { Globals.ClaimsFirstName, nameof(IdentityUser.FirstName) },
            { Globals.ClaimsLastName, nameof(IdentityUser.LastName) },
            { Globals.ClaimsProducts, nameof(IdentityUser.Products) },
            { Globals.ClaimsRoleIds, nameof(IdentityUser.Roles) },
            { Globals.ClaimsTenantId, nameof(IdentityUser.TenantId) },
            { Globals.ClaimsTenantName, nameof(IdentityUser.TenantName) },
            { Globals.ClaimsTenantDisplayName, nameof(IdentityUser.TenantDisplayName) }
        };

        public IdentityUserClaimsForwardingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Check if user is authenticated and has claims.
            if (httpContext.User.Identity.IsAuthenticated && httpContext.User.Claims.Any())
            {
                // Add all IdentityUser required claims.
                foreach (var claimToAdd in claimsToAdd)
                    AddClaimToHeader(claimToAdd, httpContext);

                foreach (var permissionClaim in httpContext.User.Claims.Where(x => x.Type.EndsWith(Globals.ClaimPermissionAppender)).GroupBy(x => x.Type))
                    httpContext.Request.Headers.Add(permissionClaim.Key, permissionClaim.Select(x => x.Value).ToArray().ToJson());
            }
            await next.Invoke(httpContext);
        }

        private static void AddClaimToHeader(KeyValuePair<string, string> claimType, HttpContext httpContext)
        {
            // Get claim value.
            var claimValue = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals(claimType.Key, StringComparison.OrdinalIgnoreCase))?.Value;

            // If the headers already contains the claim, override with value from JWT to prevent client impersonation.
            if (httpContext.Request.Headers.ContainsKey(claimType.Value))
                httpContext.Request.Headers[claimType.Value] = claimValue;
            else
                httpContext.Request.Headers.Add(claimType.Value, claimValue);
        }
    }
}
