using System;

namespace Ngx.Monorepo.Framework.Core.Interfaces
{
    /// <summary>
    /// DateTime wrapper interface that will allow us to transform datetimes we are using
    /// through out the application by adding differing concrete implementations if needed.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Returns the correct current date time for the NgxMonorepo system.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Converts a date to a string.
        /// </summary>
        /// <param name="dt">Datetime to convert to string</param>
        /// <returns>string representation of the datetime</returns>
        string ToDateString(DateTime dt);
    }
}
