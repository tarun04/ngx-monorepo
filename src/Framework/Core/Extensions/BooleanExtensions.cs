namespace Ngx.Monorepo.Framework.Core.Extensions
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// Get a boolean value as a string in the "yes"/"no" format
        /// </summary>
        /// <param name="val"></param>
        /// <returns><see cref="string"/></returns>
        public static string AsYesNo(this bool val)
        {
            return val ? "yes" : "no";
        }
    }
}
