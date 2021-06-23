using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Utilities.Validation
{
    /// <summary>
    /// Behavior used by the MediatR pipeline which intercepts and validates Commands that are send via MediatR.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetGenericTypeName();

            logger.LogInformation("Validating command {CommandType}", typeName);

            var failures = validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);

                throw new ValidationException(
                    $"Command Validation Errors for type {typeof(TRequest).Name}", failures);
            }
            return await next();
        }
    }

    /// <summary>
    /// Utility class for working with Type Names.
    /// </summary>
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// Return Generic Type Name for the given Type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Type Name as string</returns>
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;
            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }
            return typeName;
        }

        /// <summary>
        /// Return Generic Type Name for the given object.
        /// </summary>
        /// <param name="object"></param>
        /// <returns>Type Name as string</returns>
        public static string GetGenericTypeName(this object @object) => @object.GetType().GetGenericTypeName();
    }
}
