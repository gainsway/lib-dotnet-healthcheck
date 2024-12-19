using Gainsway.HealthChecks.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gainsway.HealthChecks.Extensions;

public static class IHealthChecksBuilderExtensions
{
    public static IHealthChecksBuilder AddDbHealthCheck<TDbContext>(
        this IHealthChecksBuilder builder,
        IEnumerable<string>? tags = null
    )
        where TDbContext : DbContext
    {
        tags ??= [];
        builder.AddCheck<DbHealthCheck<TDbContext>>(
            "Database health check",
            tags: [HealthCheckTags.Readiness, HealthCheckTags.Database, .. tags]
        );
        return builder;
    }
}
