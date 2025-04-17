using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Replace with your Key Vault URL
string keyVaultUrl = "https://testazkeyvaultex.vault.azure.net/";

// Create client using system-assigned managed identity
var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

// Replace with the name of your secret
string secretName = "FindMySecret";

builder.Services.AddSingleton(client);

var app = builder.Build();

app.MapGet("/", async (SecretClient client) =>
{
    KeyVaultSecret secret = await client.GetSecretAsync(secretName);
    return $"Secret Value: {secret.Value}";
});

app.Run();