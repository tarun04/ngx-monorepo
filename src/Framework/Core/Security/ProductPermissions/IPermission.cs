using System.Collections.Generic;

namespace Ngx.Monorepo.Framework.Core.Security.ProductPermissions
{
    public interface IPermission
    {
        /// <summary>
        /// Name of the product permission type.
        /// This needs to be {{Product name in Product Database}}.Permission.
        /// </summary>
        static readonly string ProductPermissionType;

        /// <summary>
        /// List of enum values for the product permission set.
        /// </summary>
        static readonly List<string> Permissions;
    }
}
