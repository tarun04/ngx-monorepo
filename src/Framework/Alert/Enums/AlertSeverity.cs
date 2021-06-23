using Ngx.Monorepo.Framework.Core.Domain;

namespace Ngx.Monorepo.Framework.Alert.Enums
{
    /// <summary>
    /// Alert Severity enum which also dictates the colors that will be used to the user when displaying an error
    /// error - red toast, 
    /// info - blue toast,
    /// success - green toast,
    /// warn - yellow toast
    /// </summary>
    public class AlertSeverity : Enumeration
    {
        public static AlertSeverity Success = new AlertSeverity(1, "success");
        public static AlertSeverity Info = new AlertSeverity(2, "info");
        public static AlertSeverity Warn = new AlertSeverity(3, "warn");
        public static AlertSeverity Error = new AlertSeverity(4, "error");

        public AlertSeverity(int id, string name) : base(id, name)
        {
        }
    }
}
