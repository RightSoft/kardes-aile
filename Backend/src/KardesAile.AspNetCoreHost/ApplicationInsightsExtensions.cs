using Azure.Monitor.OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace KardesAile.AspNetCoreHost;

public static class ApplicationInsightsExtensions
{
    public static IServiceCollection AddOpenTelemetryTracingToApplicationInsights(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry();
        
        var connectionString = configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddOpenTelemetry()
                .WithTracing(builder => builder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation(o =>
                    {
                        o.EnrichWithHttpRequest = (activity, httpRequest) =>
                        {
                            activity.SetTag("requestProtocol", httpRequest.Protocol);
                        };
                        o.EnrichWithHttpResponse = (activity, httpResponse) =>
                        {
                            activity.SetTag("responseLength", httpResponse.ContentLength);
                        };
                        o.EnrichWithException = (activity, exception) =>
                        {
                            activity.SetTag("exceptionType", exception.GetType().ToString());
                        };
                    })
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSqlClientInstrumentation(options => options.SetDbStatementForText = true)
                    .AddAzureMonitorTraceExporter(o => { o.ConnectionString = connectionString; }))
                .WithMetrics(builder => builder.AddMeter("KardesAile.AspNetCoreHost")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAzureMonitorMetricExporter(o => { o.ConnectionString = connectionString; }));
        }
        else
        {
            Console.WriteLine("Application insights is not configured");
        }

        return services;
    }
}