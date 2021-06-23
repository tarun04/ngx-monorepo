using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace Ngx.Monorepo.Framework.Core.Domain
{
    public class RequestingProductInfo
    {
        /// <summary>
        /// Id of the product.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string ProductName { get; private set; }

        /// <summary>
        /// Boolean to check whether the requesting product info is loaded
        /// </summary>
        public bool IsLoaded { get; private set; }

        public void SetProductInfoData(IDictionary<string, StringValues> headers)
        {
            if (IsLoaded)
                return;

            if (headers.TryGetValue("Requesting-ProductId", out var productId))
                this.ProductId = Guid.Parse(productId);
            if (headers.TryGetValue("Requesting-Product", out var productName))
                this.ProductName = productName;

            this.IsLoaded = true;
        }
    }
}
