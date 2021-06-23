using System;

namespace Ngx.Monorepo.Framework.Core.Security
{
    /// <summary>
    /// Encapsulates simplified product data.
    /// </summary>
    public class UserProduct
    {
        /// <summary>
        /// Id of the product this is for.
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Name of the product this is for.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Represents if the user still has access to this product.  If IsActive is false the user no longer has
        /// access to this product.
        /// </summary>
        public bool IsActive { get; }


        /// <summary>
        /// URLs for particular Tenant products that user can log into
        /// </summary>
        public string TenantProductUrl { get; }

        /// <summary>
        /// UserProduct constructor.
        /// </summary>
        /// <param name="productId"><see cref="ProductId"/></param>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="isActive"><see cref="IsActive"/></param>
        /// <param name="tenantProductUrl"><see cref="TenantProductUrl"/></param>
        public UserProduct(Guid productId, string name, bool isActive, string tenantProductUrl)
        {
            if (productId == default) throw new ArgumentException("Value cannot be default.", nameof(productId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            ProductId = productId;
            Name = name;
            IsActive = isActive;
            TenantProductUrl = tenantProductUrl;
        }
    }
}
