using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ngx.Monorepo.Framework.Extensions.Helpers
{
    /// <summary>
    /// Health check system periodically executes your health checks and calls 'PublishAsync' with the result
    /// </summary>
    public class ReadinessPublisher : IHealthCheckPublisher
    {
        private readonly ILogger _logger;
        public List<(HealthReport report, CancellationToken cancellationToken)> Entries { get; } = new List<(HealthReport report, CancellationToken cancellationToken)>();
        public Exception Exception { get; set; }

        public ReadinessPublisher(ILogger<ReadinessPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            Entries.Add((report, cancellationToken));
            _logger.LogInformation("{TIMESTAMP} Readiness Probe Status: {RESULT}", DateTime.UtcNow, report.Status);

            if (Exception != null)
            {
                throw Exception;
            }
            cancellationToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }
    }
}
