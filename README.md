# NetSig.001.Logging

SIG DotNet Refresher

## 1. Declaring and Initializing Bash Variables

```bash
#Bash - Variables
rgName="rg-sig-dotnet-refresh"
keyVaultName="kv-sig-dotnet-refresh"
secretName="connectionString"
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

## 4. Create a Secret in the Key Vault

In this example we will create a connection string secret to store its value.

```bash
az keyvault secret set --name $secretName --vault-name $keyVaultName --value "Server=localdb;Database=mydb;Authentication=Active Directory Integrated"
```
