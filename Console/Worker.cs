using Common;
using Microsoft.Extensions.Hosting;

internal class Worker : IHostedService
{
    private readonly IKeyvaultClient keyvaultClient;

    public Worker(IKeyvaultClient keyvaultClient)
    {
        this.keyvaultClient = keyvaultClient;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //TODO : Implement
        //TODO : Use ILogger
        //TODO : Use App Insights TelemetryClient here to track dependency call to Azure Key Vault
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        //TODO : Implement
        throw new NotImplementedException();
    }
}