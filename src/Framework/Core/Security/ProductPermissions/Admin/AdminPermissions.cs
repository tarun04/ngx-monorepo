using System;
using System.Collections.Generic;
using System.Linq;

namespace Ngx.Monorepo.Framework.Core.Security.ProductPermissions.Admin
{
    public class AdminPermissions : IPermission
    {
        /// <inheritdoc cref="Permissions" />
        public static readonly string ProductPermissionType = "Admin.Permission";

        /// <inheritdoc cref="ProductPermissionType" />
        public static readonly List<string> Permissions = Enum.GetValues(typeof(AdminPermissionSet)).Cast<AdminPermissionSet>()
            .Select(x => x.ToString()).ToList();
    }
}
