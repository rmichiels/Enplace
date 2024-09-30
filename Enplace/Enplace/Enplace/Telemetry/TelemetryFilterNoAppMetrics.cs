using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics;

public class TelemetryFilterNoAppMetrics : ITelemetryProcessor
{
    private ITelemetryProcessor _next;

    public TelemetryFilterNoAppMetrics(ITelemetryProcessor next)
    {
        _next = next;
    }

    public void Process(ITelemetry item)
    {
        // Check if the telemetry is of type MetricTelemetry and filter out 'AppMetrics'

        if (item is Microsoft.ApplicationInsights.DataContracts.MetricTelemetry)
        {
            return; // stop processing AppMetrics telemetry
        }

        // Continue processing other telemetry items
        _next.Process(item);
    }
}
