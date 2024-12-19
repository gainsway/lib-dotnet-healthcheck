using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Gainsway.HealthChecks.HealthChecks;

/// <summary>
/// Represents a health check for a database context.
/// </summary>
/// <typeparam name="TDbContext">The type of the database context.</typeparam>
/// <param name="dbContext">The database context to check.</param>
/// <param name="logger">The logger to log health check information.</param>
/// <seealso cref="IHealthCheck"/>
/// <remarks>
/// This health check attempts to open a connection to the database to verify its availability.
/// </remarks>
/// <example>
/// <code>
/// var dbHealthCheck = new DbHealthCheck<MyDbContext>(dbContext, logger);
/// var result = await dbHealthCheck.CheckHealthAsync(context, cancellationToken);
/// </code>
/// </example>
public class DbHealthCheck<TDbContext>(
    TDbContext dbContext,
    ILogger<DbHealthCheck<TDbContext>> logger
) : IHealthCheck
    where TDbContext : DbContext
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await dbContext.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database health check failed");
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
