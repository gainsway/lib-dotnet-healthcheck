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
        // see https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-10.0#entity-framework-core-dbcontext-probe
        builder.AddDbContextCheck<TDbContext>(
            name: "Database health check",
            tags: [HealthCheckTags.Readiness, HealthCheckTags.Database, .. tags]
        );
        return builder;
    }
}
