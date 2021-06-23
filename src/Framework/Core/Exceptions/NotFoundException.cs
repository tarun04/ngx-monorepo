using System;

namespace Ngx.Monorepo.Framework.Core.Exceptions
{
    /// <summary>
    /// Exception for a resource is not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
