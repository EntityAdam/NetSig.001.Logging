using Common;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Worker : IHostedService
{
    private readonly IKeyvaultClient _keyvaultClient;
    private readonly ILogger<Worker> _logger;
    private readonly TelemetryClient _telemetryClient;

    public Worker(IKeyvaultClient keyvaultClient, ILogger<Worker> logger, TelemetryClient telemetryClient)
    {
        _keyvaultClient = keyvaultClient;
        _logger = logger;
        _telemetryClient = telemetryClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return;
        //TODO : Implement
        //TODO : Use ILogger
        //TODO : Use App Insights TelemetryClient here to track dependency call to Azure Key Vault
        using (_telemetryClient.StartOperation<RequestTelemetry>("fetch"))
        {
            _logger.LogInformation("Starting service up");
            await _keyvaultClient.FetchConnectionStringsFromKeyvault(cancellationToken);
            _telemetryClient.TrackEvent("keyvault client completed");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //TODO : Implement
        _logger.LogInformation("Shutting service down");
        await _telemetryClient.FlushAsync(cancellationToken);
        return;
    }
}