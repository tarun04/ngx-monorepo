using Newtonsoft.Json;
using Ngx.Monorepo.Framework.Core.Exceptions;
using Ngx.Monorepo.Framework.Core.Security;
using Ngx.Monorepo.Framework.Core.Utility;
using Ngx.Monorepo.Framework.Identity.Interfaces;
using Ngx.Monorepo.Framework.Identity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Identity.Services
{
    public class IdentityB2bService : IIdentityB2bService
    {
        public static string IdentityB2bClientName => "IdentityB2b";
        private readonly HttpClient identityB2bHttpClient;
        private readonly MicroservicesHelper microservicesWrapper;
        private readonly IdentityUser user;
        private readonly JsonSerializer serializer;

        public IdentityB2bService(IHttpClientFactory httpClientFactory, MicroservicesHelper microservicesWrapper, IdentityUser user)
        {
            identityB2bHttpClient = httpClientFactory.CreateClient(IdentityB2bClientName);
            this.microservicesWrapper = microservicesWrapper;
            this.user = user;
            serializer = new JsonSerializer();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<UserModel>> GetUsersWithClaim(string product, string claim)
        {
            // Validate parameters.
            if (string.IsNullOrEmpty(product)) throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrEmpty(claim)) throw new ArgumentNullException(nameof(claim));

            // Validate user exists and is loaded.
            if (user == null || !user.IsLoaded)
                throw new NotAuthorizedException($"Method {nameof(GetUsersWithClaim)} Requires a loaded {nameof(IdentityUser)}.");

            // Get path name from app settings.
            var path = microservicesWrapper.GetPathByName(IdentityB2bClientName, "GetUsersWithClaim");

            // Check if path exists, if not throw.
            if (string.IsNullOrEmpty(path))
                throw new ConfigurationErrorsException($"Microservice Configuration For {IdentityB2bClientName} not found or is invalid." +
                    $"Please ensure you have a valid configuration set in your appsettings for the IdentityB2B Microservice!");

            // Populate claim and product into path and create HttpReqeustMessage.
            var fullPath = string.Format(path, claim, product);
            var message = new HttpRequestMessage(HttpMethod.Get, fullPath);

            // Add Identity User headers needed in the Identity B2B Microservice query.
            var headers = user.ToDictionary();
            foreach (var (key, value) in headers)
                message.Headers.Add(key, value.ToString());

            // Send request and ensure it was a successful request.
            var response = await identityB2bHttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();

            // Read response to List of UserModel and return.
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            return serializer.Deserialize<List<UserModel>>(jsonTextReader);
        }

        /// <summary>
        /// Get information about the particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUser(Guid userId)
        {
            var message = new HttpRequestMessage(HttpMethod.Get,
                string.Format(microservicesWrapper.GetPathByName("IdentityB2b", "GetUser"), userId));

            var response = await identityB2bHttpClient.SendAsync(message).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            var responseViewModel = serializer.Deserialize<UserModel>(jsonTextReader);

            return responseViewModel;
        }
    }
}
