using Ngx.Monorepo.Framework.Core.Interfaces;
using System;

namespace Ngx.Monorepo.Framework.Core.Utility
{
    /// <summary>
    /// Default implementation of the NgxMonorepo Date Time.
    /// </summary>
    public class DefaultNgxMonorepoDateTime : IDateTime
    {
        private const string dateFormat = "MM/dd/yyyy";
        private const string timeFormat = "h:mm tt";

        /// <inheritdoc />
        public DateTime Now => DateTime.UtcNow;

        /// <inheritdoc />
        public string ToDateString(DateTime dt)
        {
            return dt.ToString($"{dateFormat} {timeFormat}");
        }
    }
}
