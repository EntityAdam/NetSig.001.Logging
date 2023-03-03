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
            var keyvault = CheckConfig(configuration, "AZURE_KEYVAULT_URI");
            var keyvaultKey = CheckConfig(configuration, "AZURE_KEYVAULT_KEY");

            //TODO : Change to Logging
            logger.LogDebug($"Fetching connection string from {keyvault}");

            //TODO : Resolve nullability warning
            var client = new SecretClient(new Uri(keyvault), new DefaultAzureCredential());

            //TODO : No magic strings!
            var response = await client.GetSecretAsync(keyvaultKey);

            //TODO : Change to Logging
            logger.LogInformation($"Event Hubs Connection String: '{response.Value.Value}'");
        }

        private string CheckConfig(IConfiguration configuration, string key)
        {
            var configValue = configuration.GetValue<string>(key);

            if (configValue == null)
            {
                logger.LogError($"{key} not found in configuration");
                throw new ApplicationException($"{key} not found in configuration");
            }

            return configValue;
        }
    }
}