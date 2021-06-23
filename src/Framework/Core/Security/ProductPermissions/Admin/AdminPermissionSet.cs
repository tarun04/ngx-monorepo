using System.ComponentModel.DataAnnotations;

namespace Ngx.Monorepo.Framework.Core.Security.ProductPermissions.Admin
{
    public enum AdminPermissionSet
    {
        [Display(GroupName = "Full Access", Name = "Full Access")]
        FullAccess
    }
}
