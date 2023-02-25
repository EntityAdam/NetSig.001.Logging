# NetSig.001.Logging

SIG DotNet Refresher

## 1. Declaring and Initializing Bash Variables

```bash
#Bash - Variables
rgName="rg-sig-dotnet-refresh"
keyVaultName="kv-sig-dotnet-refresh"
keyName="connectionString"
```

## 2. Create Resource Group

```bash
az group create --name $rgName --location "EastUS"
```

## 3. Create a Key Vault

```bash
az keyvault create --name $keyVaultName --resource-group $rgName --location "EastUS"
```

After the resource is deployed, you can check the URI: https://<key-vault-name>.vault.azure.net/ 

## 4. Now we will create a Key in the Key Vault

```bash
az keyvault key create --vault-name $keyVaultName -n $keyName 
```