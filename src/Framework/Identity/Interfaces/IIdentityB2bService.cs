using Ngx.Monorepo.Framework.Core.Exceptions;
using Ngx.Monorepo.Framework.Identity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Identity.Interfaces
{
    public interface IIdentityB2bService
    {
        /// <summary>
        /// Makes a request to the Identity B2B Microservice to get all users that have the provided claim in the provided product.
        /// Using this service requires the correct appSettings.json configuration for the IdentityB2B Microservice with the correct
        /// "GetUsersWithClaim" path name specified.
        /// </summary>
        /// <param name="product">The product the claim belongs too.</param>
        /// <param name="claim">The claim value.</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of <see cref="UserModel"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if Product or Claim is null or empty.</exception>
        /// <exception cref="NotAuthorizedException">Thrown if IdentityUser is null or not loaded.</exception>
        /// <exception cref="ConfigurationErrorsException">Thrown if no Appsetting configuration exists for the Microservice "IdentityB2b" with a path specified for "GetUsersWithClaim".</exception>
        Task<IReadOnlyList<UserModel>> GetUsersWithClaim(string product, string claim);

        /// <summary>
        /// Get information about the particular Identity B2B user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserModel> GetUser(Guid userId);
    }
}
