using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Net.Http.Headers;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly SecretClient _secretClient;

    public ApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;


        var vaultUri = new Uri(_config["KeyVault:VaultUri"]);
        _secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());
    }

    private async Task<string> GetBearerTokenAsync()
    {
        string secretName = _config["KeyVault:PaknSaveToken"];
        KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
        return secret.Value;
    }

    public async Task<string> GetRetailerDataAsync(string retailer, string type, Dictionary<string, string>? parameters = null)
    {
        string token = await GetBearerTokenAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var baseUrl = _config[$"RetailApi:{retailer}:BaseUrl"];
        var endpointTemplate = _config[$"RetailApi:{retailer}:Endpoints:{type}"];

        if (parameters != null)
        {
            foreach (var kvp in parameters)
                endpointTemplate = endpointTemplate.Replace("{" + kvp.Key + "}", kvp.Value);
        }

        var fullUrl = baseUrl.TrimEnd('/') + endpointTemplate;

        var response = await _httpClient.GetAsync(fullUrl);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
