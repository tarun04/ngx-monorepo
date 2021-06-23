using Hangfire;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Core.Interfaces
{
    /// <summary>
    /// Interface that must be implemented for command handlers that are executed via Hangfire.
    /// </summary>
    public interface IHangfireJob
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="cancellationToken"><see cref="IJobCancellationToken"/>. Must be used manually.</param>
        /// <returns>N/A</returns>
        Task Execute(IJobCancellationToken cancellationToken, params object[] parameters);
    }
}
