namespace WebApiAsync.HealthCustom;

public class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;

        //get memory usage

        if (isHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy-sample result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy-sample result."));
    }
}
