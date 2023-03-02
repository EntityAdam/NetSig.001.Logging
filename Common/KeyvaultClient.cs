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

        public KeyvaultClient(IConfiguration configuration,ILogger<KeyvaultClient> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<string> FetchConnectionStringsFromKeyvault()
        {
            //Fetch Azure Key Vault URI from configuration
            var keyvault = CheckIfNull("AzureKeyVault:Uri");
            var secretName = CheckIfNull("AzureKeyVault:SecretName");

            logger.LogInformation($"Fetching connection string from {keyvault}");

            var client = new SecretClient(new Uri(keyvault), new DefaultAzureCredential());

            var response = await client.GetSecretAsync(secretName);

            //Exception:Sample - intentional
            response = null;
            
            if (response == null)
            {
                //Effective way to log
                logger.LogError(
                    AppLogEvents.ConnectionFailed,
                    "Unable to get secret from Azure Key Vault, {KeyVaultUri},{KeyVaultSecretName}",
                    keyvault,
                    secretName);

                //Not Effective
                //logger.LogError($"Unable to get the response from KeyVault: {keyvault}");
                throw new ArgumentNullException($"Empty response returned by: {nameof(client.GetSecretAsync)}");
            }
            logger.LogInformation($"Event Hubs Connection String: '{response.Value.Value}'");
            return response.Value.Value;            
        }

        private string CheckIfNull(string key)
        {
            //Logging with property: Configuration
            logger.LogInformation(AppLogEvents.Read,"Checking appsetting for key {Configuration}", key);

            var value = configuration.GetValue<string>(key);
            if (value == null)
            {
                //Logging with property: Configuration
                logger.LogError(AppLogEvents.KeyNotFound, "Key was not found {Configuration}", key);
                throw new NullReferenceException($"Key not found: {key}");
            }
            return value;
        }
    }
}