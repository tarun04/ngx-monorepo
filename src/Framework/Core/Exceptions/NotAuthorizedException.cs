using System;

namespace Ngx.Monorepo.Framework.Core.Exceptions
{
    /// <summary>
    /// Exception for when trying to access a resource with invalid credentials.
    /// </summary>
    public class NotAuthorizedException : InvalidOperationException
    {
        public NotAuthorizedException() : base() { }

        public NotAuthorizedException(string message) : base(message) { }

        public NotAuthorizedException(string message, Exception exception) : base(message, exception) { }
    }
}
