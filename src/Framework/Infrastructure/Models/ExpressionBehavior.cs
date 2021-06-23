using System.Linq.Expressions;

namespace Ngx.Monorepo.Framework.Infrastructure.Models
{
    public class ExpressionBehavior
    {
        public ExpressionType ExpressionType { get; set; }

        public bool IsBinary { get; set; }
        
        public bool UseMethod { get; set; }
        
        public string Method { get; set; }
        
        public bool MethodReturnCompareBool { get; set; }
    }
}
