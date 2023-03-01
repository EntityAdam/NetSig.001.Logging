using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common
{
    public class KeyvaultClient : IKeyvaultClient
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<KeyvaultClient> logger;

        public KeyvaultClient(IConfiguration configuration, ILogger<KeyvaultClient> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task FetchConnectionStringsFromKeyvault()
        {
            //Fetch Azure Key Vault URI from configuration
            var keyvault = configuration.GetValue<string>("AZURE_KEYVAULT_URI");

            //TODO : Change to Logging
            logger.LogInformation($"Fetching connection string from {keyvault}");

            //TODO : Resolve nullability warning
            var client = new SecretClient(new Uri(keyvault ?? "https://lnmp-kv-10.vault.azure.net/"), new DefaultAzureCredential());

            //TODO : No magic strings!
            var response = await client.GetSecretAsync("myservice-eventhubs-connectionstring");

            //TODO : Change to Logging
            logger.LogInformation($"Event Hubs Connection String: '{response.Value.Value}'");
        }
    }
}