using System;

namespace Ngx.Monorepo.Framework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get the current Unix Epoch time in seconds since 1/1/1970
        /// </summary>
        /// <param name="date"></param>
        /// <returns><see cref="long"/></returns>
        public static long ToUnixTime(this DateTime date)
        {
            var time = (date.ToUniversalTime() - new DateTime(1970, 1, 1));
            return (int)time.TotalSeconds;
        }
    }
}
