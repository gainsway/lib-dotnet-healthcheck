using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Gainsway.HealthChecks.Extensions;

public static class IWebApplicationExtensions
{
    /// <summary>
    /// Maps the health check endpoints for liveness and readiness probes.
    /// </summary>
    /// <remarks>
    /// This method sets up two health check endpoints:
    /// <list type="bullet">
    /// <item>
    /// <description><c>/healthz/ready</c> - Checks the readiness of the application. Only health checks with the "readyness" tag are included.</description>
    /// </item>
    /// <item>
    /// <description><c>/healthz/live</c> - Checks the liveness of the application. Health checks with the "readyness" tag are excluded.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public static void MapHealthChecksLiveAndReady(this WebApplication app)
    {
        app.MapHealthChecks(
            "/ready",
            new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains(HealthCheckTags.Readiness),
            }
        );

        app.MapHealthChecks(
            "/healthz",
            new HealthCheckOptions
            {
                Predicate = healthCheck =>
                    healthCheck.Tags.Contains(HealthCheckTags.Liveness)
                    || !healthCheck.Tags.Contains(HealthCheckTags.Readiness),
            }
        );
    }
}
