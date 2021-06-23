using System.Linq.Expressions;

namespace Ngx.Monorepo.Framework.Infrastructure.Models
{
    /// <summary>
    /// A filter rule used to filter data.
    /// Contains a field to be operated on, an expression type (e.g. "Equal"), and a value.
    /// </summary>
    public class FilterRule
    {
        public string Field { get; set; }

        public ExpressionBehavior Operator { get; set; }

        public string Value { get; set; }

        public ExpressionType Logic { get; set; }
    }
}
